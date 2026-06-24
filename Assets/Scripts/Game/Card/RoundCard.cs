/// <summary>
/// 回合卡牌容器类
/// 统一封装行动卡和事件卡，用于UI展示和玩家选择
/// </summary>
public class RoundCard
{
    /// <summary>卡牌类型（行动卡/事件卡）</summary>
    public CardType type;

    /// <summary>实例唯一ID，用于UI绑定和事件分发</summary>
    public int instanceId;

    /// <summary>行动卡实例数据（type为ActionCard时有效）</summary>
    public ActionCardInstance actionCard;

    /// <summary>事件卡实例数据（type为EventCard时有效）</summary>
    public EventCardInstance eventCard;

    /// <summary>
    /// 判断是否为行动卡
    /// </summary>
    public bool IsActionCard => type == CardType.ActionCard;

    /// <summary>
    /// 判断是否为事件卡
    /// </summary>
    public bool IsEventCard => type == CardType.EventCard;
}
