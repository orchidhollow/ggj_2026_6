using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class ActionCardModuleConfig : ConfigBase
{
    [LabelText("模块类型")]
    public ActionCardModuleType type;

    [LabelText("描述")]
    [TextArea(3, 6)]
    public string description;

    [LabelText("有抽取条件")]
    public bool hasDrawCondition;

    [ShowIf("hasDrawCondition")]
    [LabelText("抽取条件")]
    [ValueDropdown("@ConfigConst.GetAllDrawCondition()")]
    [SerializeReference]
    public List<DrawConditionBase> drawConditions;

    [LabelText("模块效果")]
    [ValueDropdown("@ConfigConst.GetAllEffect()")]
    [SerializeReference]
    public OptionEffectBase effect;

    [LabelText("效果图")]
    public Sprite image;
}
