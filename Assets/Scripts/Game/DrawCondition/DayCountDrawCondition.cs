using Sirenix.OdinInspector;
using System;

/// <summary>
/// 天数抽取条件类
/// 根据当前回合天数判断是否满足抽取条件
/// </summary>
[Serializable]
public class DayCountDrawCondition : DrawConditionBase
{
    /// <summary>比较方式</summary>
    [LabelText("比较方式")]
    public CompareType compareType;

    /// <summary>比较的天数</summary>
    [LabelText("天数")]
    public int value;

    /// <summary>默认构造函数</summary>
    public DayCountDrawCondition()
    {
    }

    /// <summary>
    /// 获取条件的字符串描述
    /// </summary>
    /// <returns>条件描述字符串</returns>
    public override string ToString()
    {
        var compareName = compareType.GetEnumLabel();
        return $"天数 {compareName} {value}";
    }
}
