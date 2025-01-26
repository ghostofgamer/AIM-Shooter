using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnCube : MonoBehaviour
{
    public AssetReferenceGameObject prefabReference;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            prefabReference.InstantiateAsync();
            
            
            
        }
    }
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LoadPrefab();
        }
    }

    async void LoadPrefab()
    {
        AsyncOperationHandle<GameObject> handle = prefabReference.InstantiateAsync();
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject prefabInstance = handle.Result;
            // Установите позицию или другие параметры префаба, если необходимо
            prefabInstance.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogError("Failed to load prefab");
        }
    }*/
}
