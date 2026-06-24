using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

    public class ConfigCreator<T> where T : ConfigBase
    {
#if UNITY_EDITOR
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public T config;

        private string saveFolder;
        private string saveFile;

        public ConfigCreator(string folderName, string fileName, int id)
        {
            config = ScriptableObject.CreateInstance<T>();
            config.mID = id;

            saveFolder = folderName;
            saveFile = fileName;
        }

        [Button("保存")]
        private void Save()
        {
            var folder = string.Format("{0}/{1}", GameConst.ConfigRoot, saveFolder);
            if (Directory.Exists(folder) == false)
            {
                Directory.CreateDirectory(folder);
            }

            var file = string.Format("{0}/{1}_{2}.asset", folder, saveFile, config.mID);
            if (File.Exists(file) == false)
            {
                AssetDatabase.CreateAsset(config, file);
                AssetDatabase.SaveAssets();
            }
            else
            {
                Debug.LogError("配置项已存在!! " + file);
            }
        }
#endif
    }