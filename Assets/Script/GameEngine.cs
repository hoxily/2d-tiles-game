using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    ILRuntimeManager ilruntimeManager;
    ResourceManager resourceManager;
    void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        resourceManager = gameObject.AddComponent<ResourceManager>();
        yield return resourceManager.Initialize();
        if (!resourceManager.IsInitialized)
        {
            Debug.LogError("Initialize resource manager failed.");
            yield break;
        }

        ilruntimeManager = gameObject.AddComponent<ILRuntimeManager>();
        // TODO(hoxily): ≥ı ºªØΩ≈±æ
    }
}
