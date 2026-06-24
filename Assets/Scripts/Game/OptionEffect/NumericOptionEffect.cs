using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

/// <summary>
/// 数值选项效果类
/// 表示一系列数值变化的效果
/// </summary>
[Serializable]
public class NumericOptionEffect : OptionEffectBase
{
    /// <summary>数值变化列表</summary>
    [LabelText("数值变化列表")]
    public List<NumericChange> changes;

    /// <summary>构造函数</summary>
    public NumericOptionEffect()
    {
        changes = new List<NumericChange> { };
    }

    /// <summary>
    /// 获取效果的字符串描述
    /// </summary>
    /// <returns>效果描述字符串</returns>
    public override string ToString()
    {
        var changesStr = string.Join(", ",
            changes.Select(c => {
                var valueStr = c.value > 0 ? $"+{c.value}" : c.value.ToString();
                return $"{c.numericType.GetEnumLabel()} {valueStr}";
            }));
        return $"数值变化: {changesStr}";
    }

    /// <summary>
    /// 克隆当前效果（深拷贝）
    /// </summary>
    /// <returns>新的NumericOptionEffect实例</returns>
    public NumericOptionEffect Clone()
    {
        return new NumericOptionEffect
        {
            changes = changes?.Select(c => new NumericChange
            {
                numericType = c.numericType,
                value = c.value
            }).ToList() ?? new List<NumericChange>()
        };
    }
}
