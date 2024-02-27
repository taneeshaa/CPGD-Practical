using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class AssetLoader : MonoBehaviour
{
    
    void Start()
    {
        //Addressables.LoadAssetAsync<GameObject>("Flower").Completed += OnFlowerLoaded;
        //Addressables.LoadSceneAsync("Level01").Completed += OnLevelLoaded;
    }

    private void OnLevelLoaded(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj) 
    {
        //Addressables.LoadScene(obj).Result.Scene.buildIndex);
    }
    private void OnFlowerLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Result != null)
        {
            GameObject flower = Instantiate(obj.Result);
            flower.transform.position = new Vector3(8, 1, 2);
            flower.transform.eulerAngles = new Vector3(0, 0, 0);
            flower.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Debug.Log("Flower not loaded");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("sceneTrigger"))
        {
            Addressables.LoadSceneAsync("Level01").Completed += OnLevelLoaded;
        }
        else if (other.gameObject.CompareTag("flowerTrigger"))
        {
            Addressables.LoadAssetAsync<GameObject>("Flower").Completed += OnFlowerLoaded;
        }
    }
    void Update()
    {
        
    }
}
