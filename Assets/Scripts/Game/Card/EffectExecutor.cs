using System.Collections.Generic;
using System.Linq;

using UnityEngine;

/// <summary>
/// 效果执行器类
/// 负责执行各种卡牌效果，支持prepare/delay倍率和数值变化计算
/// </summary>
public static class EffectExecutor
{
    /// <summary>
    /// 执行行动卡效果
    /// </summary>
    /// <param name="config">行动卡配置</param>
    /// <param name="isWork">是否为工作效果（true=work，false=rest）</param>
    /// <param name="prepareCount">准备次数，用于计算正面奖励倍率</param>
    public static void ExecuteActionCardEffect(
        ActionCardModuleConfig config,
        bool isWork,
        int prepareCount)
    {
        if (config == null) return;

        var effect = isWork ? config.effect : null;
        if (effect == null) return;

        ExecuteNumericOptionEffect(effect, prepareCount);
    }

    /// <summary>
    /// 执行事件卡效果
    /// </summary>
    /// <param name="config">事件卡配置</param>
    /// <param name="effectType">效果类型（Accept/Refuse/PerDelay/DelayLimit）</param>
    /// <param name="delayCount">延迟次数，用于计算正面奖励倍率</param>
    public static void ExecuteEventCardEffect(
        EventCardConfig config,
        EventCardEffectType effectType,
        int delayCount)
    {
        if (config == null) return;

        OptionEffectBase effect = effectType switch
        {
            EventCardEffectType.Accept => config.acceptEffect,
            EventCardEffectType.Refuse => config.refuseEffect,
            EventCardEffectType.PerDelay => config.perDelayEffect,
            EventCardEffectType.DelayLimit => config.delayLimitEffect,
            _ => null
        };

        if (effect == null) return;

        ExecuteNumericOptionEffect(effect, delayCount);
    }

    /// <summary>
    /// 执行数值选项效果（内部方法）
    /// </summary>
    /// <param name="effect">效果基类</param>
    /// <param name="multiplierCount">倍率计数（prepareCount或delayCount）</param>
    private static void ExecuteNumericOptionEffect(OptionEffectBase effect, int multiplierCount)
    {
        if (effect is not NumericOptionEffect numericEffect) return;
        if (numericEffect.changes == null) return;

        foreach (var change in numericEffect.changes)
        {
            // 正面奖励（值>0）受倍率影响：1 + count * 0.5
            // 负面奖励（值<=0）不受倍率影响
            int multiplier = change.value > 0 ? 1 + multiplierCount * 5 / 10 : 1;
            int finalValue = change.value * multiplier;

            if (GameManager.Instance != null && GameManager.Instance.playerInfo != null)
            {
                GameManager.Instance.playerInfo.ModifyValue(change.numericType, finalValue);
            }
        }
    }
}
