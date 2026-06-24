using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class EventCardConfig : ConfigBase
{
    [LabelText("有抽取条件")]
    public bool hasDrawCondition;

    [LabelText("描述")]
    [TextArea(3, 6)]
    public string description;

    [ShowIf("hasDrawCondition")]
    [LabelText("条件列表")]
    [ValueDropdown("@ConfigConst.GetAllDrawCondition()")]
    [SerializeReference]
    public List<DrawConditionBase> conditions;

    [LabelText("Accept效果")]
    [ValueDropdown("@ConfigConst.GetAllEffect()")]
    [SerializeReference]
    public OptionEffectBase acceptEffect;

    [LabelText("Refuse效果")]
    [ValueDropdown("@ConfigConst.GetAllEffect()")]
    [SerializeReference]
    public OptionEffectBase refuseEffect;

    [LabelText("每次Delay效果")]
    [ValueDropdown("@ConfigConst.GetAllEffect()")]
    [SerializeReference]
    public OptionEffectBase perDelayEffect;

    [LabelText("Delay上限效果")]
    [ValueDropdown("@ConfigConst.GetAllEffect()")]
    [SerializeReference]
    public OptionEffectBase delayLimitEffect;

    [LabelText("效果图")]
    public Sprite image;
}