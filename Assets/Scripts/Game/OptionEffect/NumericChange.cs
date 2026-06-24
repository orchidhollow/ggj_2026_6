using System;
using Sirenix.OdinInspector;

/// <summary>
/// 数值变化类
/// 表示对某种数值的增加或减少
/// </summary>
[Serializable]
public class NumericChange
{
    /// <summary>数值类型（Health/Spirit/Money/Reputation）</summary>
    [LabelText("数值类型")]
    public NumericType numericType;

    /// <summary>变化的数值（正数为增加，负数为减少）</summary>
    [LabelText("数值")]
    public int value;
}
