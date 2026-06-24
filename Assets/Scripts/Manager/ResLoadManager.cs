using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResLoadManager : BaseManager<ResLoadManager>
{
    private ResLoadManager() { }

    public void Clear()
    {
        foreach (var objs in _cacheObj)
        {
            var list = objs.Value;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var o = list[i];
                if (o != null) Object.Destroy(o);
            }
        }

        _cacheObj.Clear();

    }

    /// <summary>
    /// 物体缓存
    /// </summary>
    private Dictionary<string, List<GameObject>> _cacheObj = new();

    /// <summary>
    /// 异步加载实例化
    /// </summary>
    /// <param name="path">Resources下的路径（不含扩展名）</param>
    /// <param name="parent"></param>
    /// <param name="show"></param>
    /// <returns></returns>
    public async UniTask<GameObject> CreateGameObject(
        string path,
        Transform parent = null,
        bool show = true)
    {
        if (string.IsNullOrEmpty(path)) return null;

        GameObject obj = null;
        if (_cacheObj.TryGetValue(path, out var list))
        {
            if (list.Count > 0)
            {
                obj = list[0];
                list.RemoveAt(0);
            }
        }

        if (obj == null)
        {
            var prefab = Resources.Load<GameObject>(path);
            if (prefab == null)
            {
                Debug.LogError($"ResLoadManager: 无法加载资源 {path}");
                return null;
            }
            obj = Object.Instantiate(prefab, parent);
        }

        obj.transform.SetParent(parent);
        obj.gameObject.SetActive(show);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        return obj;
    }

    /// <summary>
    /// 异步加载实例化
    /// </summary>
    /// <param name="prefab">预制体引用</param>
    /// <param name="parent"></param>
    /// <param name="show"></param>
    /// <returns></returns>
    public async UniTask<GameObject> CreateGameObject(
        GameObject prefab,
        Transform parent = null,
        bool show = true)
    {
        if (prefab == null) return null;

        var path = prefab.name;
        GameObject obj = null;
        if (_cacheObj.TryGetValue(path, out var list))
        {
            if (list.Count > 0)
            {
                obj = list[0];
                list.RemoveAt(0);
            }
        }

        if (obj == null)
        {
            obj = Object.Instantiate(prefab, parent);
        }

        obj.transform.SetParent(parent);
        obj.gameObject.SetActive(show);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        return obj;
    }

    /// <summary>
    /// 回收物体
    /// </summary>
    /// <param name="path"></param>
    /// <param name="obj"></param>
    /// <param name="cache"></param>
    public void CacheGameObject(string path, GameObject obj, bool cache = true)
    {
        if (obj == null)
        {
            Debug.LogError("回收了空模型 " + path);
            return;
        }

        if (cache == false)
        {
            Object.Destroy(obj);
            return;
        }

        if (_cacheObj.TryGetValue(path, out var list) == false)
        {
            list = new List<GameObject>();
            _cacheObj.Add(path, list);
        }

        if (list.Contains(obj))
        {
            Debug.LogError($"已缓存该模型 不再缓存 忽略 {path} {obj.name}");
            return;
        }

        list.Add(obj);
        //obj.transform.SetParent(GameConst.mCacheRoot);
        obj.transform.localScale = Vector3.one;
        obj.transform.localEulerAngles = Vector3.zero;
        obj.SetActive(false);
    }
}
