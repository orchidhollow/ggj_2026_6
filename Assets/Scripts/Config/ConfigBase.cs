using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

    [Serializable]
    public class ConfigBase : ScriptableObject
    {
        [ReadOnly, LabelText("ID")] public int mID;

        [LabelText("名称备注")] public string mCommon;

#if UNITY_EDITOR
        public void Save()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        public virtual void Preview()
        {
        }

        public override string ToString()
        {
            return $"({mCommon} : {mID})";
        }
#endif
    }