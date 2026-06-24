using System.Linq;
using UnityEditor;
using UnityEngine;

public static class RandomConfigGenerator
{
    [MenuItem("Tools/配置/随机生成所有配置")]
    public static void GenerateRandomConfigs()
    {
        GenerateAllActionCardModules();
        GenerateAllEventCards();
        AssetDatabase.Refresh();
        Debug.Log("配置生成完成！");
    }

    private static void GenerateAllActionCardModules()
    {
        var ids = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        foreach (var id in ids)
        {
            GenerateActionCardModule(id);
        }
        Debug.Log($"行动模块配置生成完成，共{ids.Length}个");
    }

    private static void GenerateAllEventCards()
    {
        var ids = Enumerable.Range(1, 21).ToArray();
        foreach (var id in ids)
        {
            GenerateEventCard(id);
        }
        Debug.Log($"事件卡配置生成完成，共{ids.Length}个");
    }

    private static void GenerateActionCardModule(int id)
    {
        var config = AssetDatabase.LoadAssetAtPath<ActionCardModuleConfig>(
            $"Assets/Resource/Config/ActionCardModule/ActionCardModuleConfig_{id}.asset");
        
        if (config == null)
        {
            Debug.LogWarning($"未找到行动模块配置: ID={id}");
            return;
        }

        config.hasDrawCondition = false;
        config.description = config.mCommon;
        config.effect = CreateRandomEffect();
        
        EditorUtility.SetDirty(config);
    }

    private static void GenerateEventCard(int id)
    {
        var config = AssetDatabase.LoadAssetAtPath<EventCardConfig>(
            $"Assets/Resource/Config/EventCard/EventCardConfig_{id}.asset");
        
        if (config == null)
        {
            Debug.LogWarning($"未找到事件卡配置: ID={id}");
            return;
        }

        config.hasDrawCondition = false;
        config.description = config.mCommon;
        config.acceptEffect = CreateRandomEffect();
        config.refuseEffect = CreateRandomEffect();
        config.perDelayEffect = CreateRandomEffect();
        config.delayLimitEffect = CreateRandomEffect();
        
        EditorUtility.SetDirty(config);
    }

    private static NumericOptionEffect CreateRandomEffect()
    {
        var effect = new NumericOptionEffect();
        
        int changeCount = Random.Range(1, 4);
        for (int i = 0; i < changeCount; i++)
        {
            int typeIndex = Random.Range(0, 4);
            var numericType = (NumericType)typeIndex;
            
            int value = 0;
            while (value == 0)
            {
                value = Random.Range(-10, 11);
            }
            
            var change = new NumericChange
            {
                numericType = numericType,
                value = value
            };
            effect.changes.Add(change);
        }
        
        return effect;
    }
}
