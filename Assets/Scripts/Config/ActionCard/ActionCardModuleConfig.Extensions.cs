using System.Collections.Generic;

/// <summary>
/// 行动卡模块配置扩展方法类
/// 提供类型判断和分类筛选功能
/// </summary>
public static class ActionCardModuleConfigExtensions
{
    /// <summary>
    /// 判断配置是否为工作类型
    /// </summary>
    /// <param name="config">行动卡模块配置</param>
    /// <returns>是否为工作类型</returns>
    public static bool IsWorkType(this ActionCardModuleConfig config)
    {
        return config?.type == ActionCardModuleType.Work;
    }

    /// <summary>
    /// 判断配置是否为休息类型
    /// </summary>
    /// <param name="config">行动卡模块配置</param>
    /// <returns>是否为休息类型</returns>
    public static bool IsRestType(this ActionCardModuleConfig config)
    {
        return config?.type == ActionCardModuleType.Rest;
    }

    /// <summary>
    /// 从概览中获取所有工作类型配置
    /// </summary>
    /// <param name="overview">行动卡模块概览</param>
    /// <returns>工作类型配置列表</returns>
    public static List<ActionCardModuleConfig> GetWorkConfigs(this ActionCardModuleOverview overview)
    {
        var result = new List<ActionCardModuleConfig>();
        if (overview == null) return result;

        for (int i = 0; i < overview.AllKeys.Count; i++)
        {
            var config = overview.AllValues[i];
            if (config.IsWorkType())
            {
                result.Add(config);
            }
        }
        return result;
    }

    /// <summary>
    /// 从概览中获取所有休息类型配置
    /// </summary>
    /// <param name="overview">行动卡模块概览</param>
    /// <returns>休息类型配置列表</returns>
    public static List<ActionCardModuleConfig> GetRestConfigs(this ActionCardModuleOverview overview)
    {
        var result = new List<ActionCardModuleConfig>();
        if (overview == null) return result;

        for (int i = 0; i < overview.AllKeys.Count; i++)
        {
            var config = overview.AllValues[i];
            if (config.IsRestType())
            {
                result.Add(config);
            }
        }
        return result;
    }
}
