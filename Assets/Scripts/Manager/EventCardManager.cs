using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 事件卡管理器
/// 负责管理所有事件卡配置和随机抽取功能
/// </summary>
public class EventCardManager : BaseManager<EventCardManager>
{
    /// <summary>所有事件卡配置字典（ID -> 配置）</summary>
    public Dictionary<int, EventCardConfig> AllCards { get; private set; }

    /// <summary>配置运行时加载器</summary>
    private ConfigRuntime<int, EventCardConfig> _configRuntime;

    /// <summary>私有构造函数（单例模式）</summary>
    private EventCardManager()
    {
        AllCards = new Dictionary<int, EventCardConfig>();
        _configRuntime = new ConfigRuntime<int, EventCardConfig>();
    }

    /// <summary>
    /// 初始化加载所有配置
    /// </summary>
    public UniTask Init()
    {
        _configRuntime.LoadConfigOverview(EventCardOverview.Instance, loadAll: true);
        LoadAllCards();
        return UniTask.CompletedTask;
    }

    /// <summary>
    /// 加载所有事件卡配置
    /// </summary>
    private void LoadAllCards()
    {
        AllCards.Clear();

        var overview = EventCardOverview.Instance;
        for (int i = 0; i < overview.AllKeys.Count; i++)
        {
            var key = overview.AllKeys[i];
            var config = overview.AllValues[i];
            AllCards[key] = config;
        }
    }

    /// <summary>
    /// 根据ID获取事件卡配置
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>事件卡配置，未找到返回null</returns>
    public EventCardConfig GetCard(int id)
    {
        return AllCards.TryGetValue(id, out var config) ? config : null;
    }

    /// <summary>
    /// 随机获取一个事件卡配置ID
    /// </summary>
    /// <returns>事件卡配置ID，无配置时返回0</returns>
    public int GetRandomEventConfigId()
    {
        if (AllCards.Count == 0) return 0;
        var keys = new List<int>(AllCards.Keys);
        return keys[Random.Range(0, keys.Count)];
    }
}
