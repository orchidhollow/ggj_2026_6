using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 游戏管理器（单例类）
/// 核心管理回合流程、卡牌缓存池、玩家状态和卡牌选择逻辑
/// </summary>
public class GameManager : SingletonMono<GameManager>
{
    /// <summary>玩家信息（属性值）</summary>
    public PlayerInfo playerInfo;

    /// <summary>当前回合数</summary>
    private int _turnCount = 0;

    /// <summary>当前回合卡牌索引（指向正在处理的卡）</summary>
    private int _currentCardIndex = 0;

    /// <summary>实例ID计数器（用于生成唯一ID）</summary>
    private int _instanceIdCounter = 0;

    /// <summary>行动卡缓存池（存放prepare后的卡）</summary>
    private List<ActionCardInstance> _actionCardPool = new List<ActionCardInstance>();

    /// <summary>事件卡缓存池（存放delay后的卡）</summary>
    private List<EventCardInstance> _eventCardPool = new List<EventCardInstance>();

    /// <summary>当前回合的所有卡牌容器</summary>
    private List<RoundCard> _roundCards = new List<RoundCard>();

    /// <summary>
    /// 获取当前回合数
    /// </summary>
    public int turnCount => _turnCount;

    public bool init;

    /// <summary>
    /// 初始化方法
    /// 1. 初始化玩家属性
    /// 2. 加载行动卡和事件卡配置
    /// </summary>
    protected override async void Awake()
    {
        base.Awake();
        playerInfo = new PlayerInfo(60, 60, 60, 60);
        await ActionCardManager.Instance.Init();
        await EventCardManager.Instance.Init();
        Debug.Log("加载所有配置成功");
        init = true;
    }

    /// <summary>
    /// 开始新回合
    /// 1. 回合数+1
    /// 2. 从缓存池补充行动卡
    /// 3. 抽取不足的行动卡
    /// 4. 抽取2张事件卡
    /// 5. 通知UI展示第一张卡
    /// </summary>
    public void StartNewTurn()
    {
        _turnCount++;
        _currentCardIndex = 0;
        _roundCards.Clear();

        int actionCardsNeeded = 4;

        // 从缓存池中取卡，优先使用缓存
        while (_actionCardPool.Count > 0 && actionCardsNeeded > 0)
        {
            var card = _actionCardPool[0];
            _actionCardPool.RemoveAt(0);
            _roundCards.Add(new RoundCard
            {
                type = CardType.ActionCard,
                instanceId = GenerateInstanceId(),
                actionCard = card
            });
            actionCardsNeeded--;
        }

        // 抽取不足的行动卡
        for (int i = 0; i < actionCardsNeeded; i++)
        {
            _roundCards.Add(CreateNewActionCard());
        }

        // 抽取事件卡（每回合2张，优先从缓存池取）
        int eventCardsNeeded = 2;
        while (_eventCardPool.Count > 0 && eventCardsNeeded > 0)
        {
            var card = _eventCardPool[0];
            _eventCardPool.RemoveAt(0);
            _roundCards.Add(new RoundCard
            {
                type = CardType.EventCard,
                instanceId = GenerateInstanceId(),
                eventCard = card
            });
            eventCardsNeeded--;
        }

        // 抽取不足的事件卡
        for (int i = 0; i < eventCardsNeeded; i++)
        {
            _roundCards.Add(CreateNewEventCard());
        }

        // 通知UI展示第一张卡
        NotifyShowNextCard();
    }

    /// <summary>
    /// 创建新的行动卡实例
    /// 从Work和Rest类型配置中各随机抽取一个
    /// </summary>
    /// <returns>新的行动卡容器</returns>
    private RoundCard CreateNewActionCard()
    {
        int workId = ActionCardManager.Instance.GetRandomWorkConfigId();
        int restId = ActionCardManager.Instance.GetRandomRestConfigId();

        return new RoundCard
        {
            type = CardType.ActionCard,
            instanceId = GenerateInstanceId(),
            actionCard = new ActionCardInstance(workId, restId)
        };
    }

    /// <summary>
    /// 创建新的事件卡实例
    /// 从事件卡配置中随机抽取一个
    /// </summary>
    /// <returns>新的事件卡容器</returns>
    private RoundCard CreateNewEventCard()
    {
        int configId = EventCardManager.Instance.GetRandomEventConfigId();

        return new RoundCard
        {
            type = CardType.EventCard,
            instanceId = GenerateInstanceId(),
            eventCard = new EventCardInstance(configId)
        };
    }

    /// <summary>
    /// 玩家选择行动卡选项
    /// </summary>
    /// <param name="instanceId">行动卡实例ID</param>
    /// <param name="option">选择的选项（Work/Rest/Prepare）</param>
    public void SelectActionCardOption(int instanceId, ActionCardOption option)
    {
        var card = _roundCards.Find(c => c.instanceId == instanceId);
        if (card == null || !card.IsActionCard) return;

        var instance = card.actionCard;
        var workConfig = ActionCardManager.Instance.GetWorkCard(instance.workConfigId);
        var restConfig = ActionCardManager.Instance.GetRestCard(instance.restConfigId);

        switch (option)
        {
            case ActionCardOption.Work:
                // 执行工作效果，受prepareCount倍率影响
                EffectExecutor.ExecuteActionCardEffect(workConfig, true, instance.prepareCount);
                break;

            case ActionCardOption.Rest:
                // 执行休息效果，受prepareCount倍率影响
                EffectExecutor.ExecuteActionCardEffect(restConfig, false, instance.prepareCount);
                break;

            case ActionCardOption.Prepare:
                // prepare次数+1
                instance.prepareCount++;
                // prepareCount >= 3 时直接丢弃，否则放入缓存池
                if (instance.prepareCount < 3)
                {
                    _actionCardPool.Add(instance);
                    Debug.Log($"当前行动卡prepare数:{instance.prepareCount},放入缓存池");
                }
                else
                {
                    Debug.Log("当前行动卡prepare回合数等于3，直接丢弃");
                }
                break;
        }

        // 处理完毕，移动到下一张卡
        MoveToNextCard();
    }

    /// <summary>
    /// 玩家选择事件卡选项
    /// </summary>
    /// <param name="instanceId">事件卡实例ID</param>
    /// <param name="option">选择的选项（Accept/Refuse/Delay）</param>
    public void SelectEventCardOption(int instanceId, EventCardOption option)
    {
        var card = _roundCards.Find(c => c.instanceId == instanceId);
        if (card == null || !card.IsEventCard) return;

        var instance = card.eventCard;
        var config = EventCardManager.Instance.GetCard(instance.configId);

        switch (option)
        {
            case EventCardOption.Accept:
                // 执行接受效果
                EffectExecutor.ExecuteEventCardEffect(config, EventCardEffectType.Accept, instance.delayCount);
                break;

            case EventCardOption.Refuse:
                // 执行拒绝效果
                EffectExecutor.ExecuteEventCardEffect(config, EventCardEffectType.Refuse, instance.delayCount);
                break;

            case EventCardOption.Delay:
                // 执行延迟效果
                EffectExecutor.ExecuteEventCardEffect(config, EventCardEffectType.PerDelay, instance.delayCount);
                // delay次数+1
                instance.delayCount++;

                // delayCount >= 3 时执行上限效果并结束此卡，否则放入缓存池
                if (instance.delayCount >= 3)
                {
                    Debug.Log("当前事件卡delay数等于3，直接丢弃");
                    EffectExecutor.ExecuteEventCardEffect(config, EventCardEffectType.DelayLimit, instance.delayCount);
                    MoveToNextCard();
                    return;
                }
                else
                {
                    Debug.Log($"当前事件卡delay数:{instance.delayCount},放入缓存池");
                    _eventCardPool.Add(instance);
                }
                break;
        }

        // Delay < 3 时继续展示当前卡供选择
        MoveToNextCard();
    }

    /// <summary>
    /// 移动到下一张卡
    /// </summary>
    private void MoveToNextCard()
    {
        _currentCardIndex++;
        NotifyShowNextCard();
    }

    /// <summary>
    /// 通知UI展示下一张卡
    /// 如果所有卡都已处理完毕，触发回合结束事件
    /// </summary>
    private void NotifyShowNextCard()
    {
        if (_currentCardIndex < _roundCards.Count)
        {
            // 通知UI展示当前索引的卡
            EventCenter.Instance.EventTrigger(E_EventType.E_ShowNextCard, _roundCards[_currentCardIndex]);
        }
        else
        {
            // 所有卡处理完毕，触发回合结束事件
            EventCenter.Instance.EventTrigger(E_EventType.E_RoundFinished);
        }
    }

    /// <summary>
    /// 生成唯一的实例ID
    /// 格式: turnCount * 10000 + counter
    /// </summary>
    /// <returns>唯一实例ID</returns>
    private int GenerateInstanceId()
    {
        return _turnCount * 10000 + ++_instanceIdCounter;
    }

    /// <summary>
    /// 获取缓存池中prepare卡的数量
    /// </summary>
    /// <returns>缓存池中行动卡的数量</returns>
    public int GetPoolPrepareCardCount()
    {
        return _actionCardPool.Count;
    }

    /// <summary>
    /// 获取缓存池中delay卡的数量
    /// </summary>
    /// <returns>缓存池中事件卡的数量</returns>
    public int GetPoolDelayCardCount()
    {
        return _eventCardPool.Count;
    }
}
