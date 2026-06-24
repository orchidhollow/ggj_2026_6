using Sirenix.OdinInspector;
using System;

/// <summary>
/// 数值抽取条件类
/// 根据玩家数值属性判断是否满足抽取条件
/// </summary>
[Serializable]
public class NumericDrawCondition : DrawConditionBase
{
    /// <summary>数值类型</summary>
    [LabelText("数值类型")]
    public NumericType numericType;

    /// <summary>比较方式</summary>
    [LabelText("比较方式")]
    public CompareType compareType;

    /// <summary>比较的数值</summary>
    [LabelText("数值")]
    public int value;

    /// <summary>
    /// 获取条件的字符串描述
    /// </summary>
    /// <returns>条件描述字符串</returns>
    public override string ToString()
    {
        var typeName = numericType.GetEnumLabel();
        var compareName = compareType.GetEnumLabel();
        return $"{typeName} {compareName} {value}";
    }
}
