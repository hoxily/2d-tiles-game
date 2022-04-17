using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviourSingleton<ResourceManager>
{
    bool isInitialized;

    public Coroutine Initialize()
    {
        return StartCoroutine(InitializeAsync());
    }

    public bool IsInitialized
    {
        get
        {
            return isInitialized;
        }
    }

    IEnumerator InitializeAsync()
    {
        // TODO(hoxily): 首次运行初始化、网络资源比对下载。
        isInitialized = true;
        yield break;
    }

    public T LoadAsset<T>(string assetPath) where T : Object
    {
#if UNITY_EDITOR
        return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(assetPath);
#else
        // TODO(hoxily): load from assetbundles
#endif
    }
}
