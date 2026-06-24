using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;

    public class ConfigRuntime<T1, T2> where T2 : ConfigBase
    {
        /// <summary>
        /// 配置Overview
        /// </summary>
        public IConfigOverview<T1, T2> ConfigOverview { get; private set; }

        /// <summary>
        /// 运行过程中已加载的配置
        /// </summary>
        public Dictionary<T1, T2> RuntimeConfigs { get; } = new();

        /// <summary>
        /// 加载ConfigOverview
        /// </summary>
        /// <param name="overview">配置总览实例</param>
        /// <param name="loadAll">加载全部配置</param>
        public void LoadConfigOverview(IConfigOverview<T1, T2> overview, bool loadAll = false)
        {
            ConfigOverview = overview;

            if (loadAll) LoadAllConfig();
        }

        /// <summary>
        /// 加载全部配置
        /// </summary>
        /// <returns></returns>
        private void LoadAllConfig()
        {
            for (int i = 0; i < ConfigOverview.AllKeys.Count; i++)
            {
                var key = ConfigOverview.AllKeys[i];
                if (RuntimeConfigs.ContainsKey(key)) continue;

                RuntimeConfigs[key] = ConfigOverview.AllValues[i];
            }
        }

        /// <summary>
        /// 根据id获取配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T2 GetConfigById(T1 key)
        {
            if (RuntimeConfigs.TryGetValue(key, out var cfg))
            {
                return cfg;
            }

            for (int i = 0; i < ConfigOverview.AllKeys.Count; i++)
            {
                if (Equals(ConfigOverview.AllKeys[i], key))
                {
                    cfg = ConfigOverview.AllValues[i];
                    RuntimeConfigs[key] = cfg;
                    return cfg;
                }
            }

            return null;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="contains">包含</param>
        public void Release(params T1[] contains)
        {
            if (contains.Length > 0)
            {
                foreach (var key in contains)
                {
                    RuntimeConfigs.Remove(key);
                }
            }
            else
            {
                RuntimeConfigs.Clear();
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="ignores">忽略</param>
        public void Release2(params T1[] ignores)
        {
            if (ignores.Length > 0)
            {
                var keys = ListPool<T1>.Get();

                foreach (var key in RuntimeConfigs.Keys)
                {
                    if (ignores.Contains(key) == false)
                    {
                        keys.Add(key);
                    }
                }

                for (int i = 0; i < keys.Count; i++)
                {
                    RuntimeConfigs.Remove(keys[i]);
                }

                ListPool<T1>.Release(keys);
            }
            else
            {
                RuntimeConfigs.Clear();
            }
        }
    }
