using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[Serializable, GlobalConfig("Assets/Resource/Config/Global")]
public class EventCardOverview : CustomGlobalConfig<EventCardOverview, EventCardConfig>, IConfigOverview<int, EventCardConfig>
{
    [field: SerializeField, ReadOnly] public string FolderName { get; private set; } = "EventCard";
    [field: SerializeField, ReadOnly] public int NextBaseConfigId { get; private set; } = 1;
    [field: SerializeField, ReadOnly] public List<int> AllKeys { get; set; } = new();
    [field: SerializeField, ReadOnly] public List<EventCardConfig> AllValues { get; set; } = new();

#if UNITY_EDITOR
    public override void UpdateConfigOverview()
    {
        NextBaseConfigId = UpdateConfigOverview(FolderName, AllKeys, AllValues);
    }

    public EventCardConfig GetConfig(int id)
    {
        for (int i = 0; i < AllKeys.Count; i++)
        {
            if (id == AllKeys[i])
            {
                return AllValues[i];
            }
        }

        return null;
    }

    public string GetCommon(int id)
    {
        for (int i = 0; i < AllKeys.Count; i++)
        {
            if (id == AllKeys[i])
            {
                return AllValues[i].mCommon;
            }
        }

        return "";
    }
#endif
}
