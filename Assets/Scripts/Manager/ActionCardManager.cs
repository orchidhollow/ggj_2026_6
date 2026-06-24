using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 行动卡管理器
/// 负责管理所有行动卡配置和随机抽取功能
/// </summary>
public class ActionCardManager : BaseManager<ActionCardManager>
{
    /// <summary>所有行动卡配置字典（ID -> 配置）</summary>
    public Dictionary<int, ActionCardModuleConfig> AllCards { get; private set; }

    /// <summary>工作类型行动卡配置字典（ID -> 配置）</summary>
    public Dictionary<int, ActionCardModuleConfig> WorkCards { get; private set; }

    /// <summary>休息类型行动卡配置字典（ID -> 配置）</summary>
    public Dictionary<int, ActionCardModuleConfig> RestCards { get; private set; }

    /// <summary>配置运行时加载器</summary>
    private ConfigRuntime<int, ActionCardModuleConfig> _configRuntime;

    /// <summary>私有构造函数（单例模式）</summary>
    private ActionCardManager()
    {
        AllCards = new Dictionary<int, ActionCardModuleConfig>();
        WorkCards = new Dictionary<int, ActionCardModuleConfig>();
        RestCards = new Dictionary<int, ActionCardModuleConfig>();
        _configRuntime = new ConfigRuntime<int, ActionCardModuleConfig>();
    }

    /// <summary>
    /// 初始化加载所有配置
    /// </summary>
    public UniTask Init()
    {
        _configRuntime.LoadConfigOverview(ActionCardModuleOverview.Instance, loadAll: true);
        LoadAllCards();
        return UniTask.CompletedTask;
    }

    /// <summary>
    /// 加载并分类所有行动卡配置
    /// </summary>
    private void LoadAllCards()
    {
        AllCards.Clear();
        WorkCards.Clear();
        RestCards.Clear();

        var overview = ActionCardModuleOverview.Instance;
        for (int i = 0; i < overview.AllKeys.Count; i++)
        {
            var key = overview.AllKeys[i];
            var config = overview.AllValues[i];

            AllCards[key] = config;

            if (config.type == ActionCardModuleType.Work)
                WorkCards[key] = config;
            else if (config.type == ActionCardModuleType.Rest)
                RestCards[key] = config;
        }
    }

    /// <summary>
    /// 根据ID获取行动卡配置
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>行动卡配置，未找到返回null</returns>
    public ActionCardModuleConfig GetCard(int id)
    {
        return AllCards.TryGetValue(id, out var config) ? config : null;
    }

    /// <summary>
    /// 根据ID获取工作类型行动卡配置
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>工作类型行动卡配置，未找到返回null</returns>
    public ActionCardModuleConfig GetWorkCard(int id)
    {
        return WorkCards.TryGetValue(id, out var config) ? config : null;
    }

    /// <summary>
    /// 根据ID获取休息类型行动卡配置
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>休息类型行动卡配置，未找到返回null</returns>
    public ActionCardModuleConfig GetRestCard(int id)
    {
        return RestCards.TryGetValue(id, out var config) ? config : null;
    }

    /// <summary>
    /// 随机获取一个工作类型配置ID
    /// </summary>
    /// <returns>工作类型配置ID，无配置时返回0</returns>
    public int GetRandomWorkConfigId()
    {
        if (WorkCards.Count == 0) return 0;
        var keys = new List<int>(WorkCards.Keys);
        return keys[Random.Range(0, keys.Count)];
    }

    /// <summary>
    /// 随机获取一个休息类型配置ID
    /// </summary>
    /// <returns>休息类型配置ID，无配置时返回0</returns>
    public int GetRandomRestConfigId()
    {
        if (RestCards.Count == 0) return 0;
        var keys = new List<int>(RestCards.Keys);
        return keys[Random.Range(0, keys.Count)];
    }
}
