using System.Collections.Generic;
using System.IO;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

    public abstract class CustomGlobalConfig<T, T2> : ScriptableObject
        where T : CustomGlobalConfig<T, T2>, new()
        where T2 : ConfigBase
    {
        public static T Instance => GlobalConfigUtility<T>.GetInstance(ConfigHelper.ConfigAttribute(typeof(T)).AssetPath);

#if UNITY_EDITOR
        protected int UpdateConfigOverview(string FolderName, List<int> AllKeys, List<T2> AllValues, int editorNum = 0)
        {
            AllKeys.Clear();
            AllValues.Clear();

            var path = string.Format("{0}/{1}", GameConst.ConfigRoot, FolderName);
            if (Directory.Exists(path) == false) return 1;

            int max = 0;

            var files = new DirectoryInfo(path).GetFiles();
            foreach (var file in files)
            {
                if (file.Name.EndsWith(".meta")) continue;

                var str = file.FullName.Replace(@"\", "/");
                var p = str.Replace(Application.dataPath, "Assets");

                var tmpData = AssetDatabase.LoadAssetAtPath<T2>(p);
                if (tmpData == null) continue;
                AllValues.Add(tmpData);

                if (tmpData.mID > max) max = tmpData.mID;
            }

            AllValues.Sort((first, second) =>
            {
                if (first.mID < second.mID)
                    return -1;
                return 1;
            });

            foreach (var asset in AllValues)
            {
                AllKeys.Add(asset.mID);
            }

            var NextBaseConfigId = 1;
            if (max < editorNum) max = editorNum;
            for (int i = 1; i <= max + 1; i++)
            {
                if (AllKeys.Contains(i) == false)
                {
                    if (editorNum == 0 || i >= editorNum)
                    {
                        NextBaseConfigId = i;
                        break;
                    }
                }
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();

            return NextBaseConfigId;
        }

        public abstract void UpdateConfigOverview();
#endif
    }
