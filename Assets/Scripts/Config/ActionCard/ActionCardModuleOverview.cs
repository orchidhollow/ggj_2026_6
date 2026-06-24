using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[Serializable, GlobalConfig("Assets/Resource/Config/Global")]
public class ActionCardModuleOverview : CustomGlobalConfig<ActionCardModuleOverview, ActionCardModuleConfig>, IConfigOverview<int, ActionCardModuleConfig>
{
    [field: SerializeField, ReadOnly] public string FolderName { get; private set; } = "ActionCardModule";
    [field: SerializeField, ReadOnly] public int NextBaseConfigId { get; private set; } = 1;
    [field: SerializeField, ReadOnly] public List<int> AllKeys { get; set; } = new();
    [field: SerializeField, ReadOnly] public List<ActionCardModuleConfig> AllValues { get; set; } = new();

#if UNITY_EDITOR
    public override void UpdateConfigOverview()
    {
        NextBaseConfigId = UpdateConfigOverview(FolderName, AllKeys, AllValues);
    }

    public ActionCardModuleConfig GetConfig(int id)
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
