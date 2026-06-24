using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public static class ConfigConst
{
#if UNITY_EDITOR
    /// <summary>
    /// 返回所有抽取条件
    /// </summary>
    /// <returns></returns>
    public static ValueDropdownList<DrawConditionBase> GetAllDrawCondition()
    {
        var list = new ValueDropdownList<DrawConditionBase>();
        list.Add("数值条件", new NumericDrawCondition());
        list.Add("天数条件", new DayCountDrawCondition());
        return list;
    }

    /// <summary>
    /// 返回所有选项效果
    /// </summary>
    /// <returns></returns>
    public static ValueDropdownList<OptionEffectBase> GetAllOptionEffect()
    {
        var list = new ValueDropdownList<OptionEffectBase>();
        list.Add("数值变化", new NumericOptionEffect());
        return list;
    }

    /// <summary>
    /// 返回所有卡牌效果
    /// </summary>
    /// <returns></returns>
    public static ValueDropdownList<OptionEffectBase> GetAllEffect()
    {
        var list = new ValueDropdownList<OptionEffectBase>();
        list.Add("数值变化", new NumericOptionEffect());
        return list;
    }
#endif
}
