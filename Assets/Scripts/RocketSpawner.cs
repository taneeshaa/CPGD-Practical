using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnables;
    [SerializeField] List<Transform> spawnTransform = new List<Transform>();
    private float interval = 3f;
    private float timer = 0f;
    private int randomObj;
    private int randomX;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > interval)
        {
            randomObj = Random.Range(0, spawnables.Count);
            randomX = Random.Range(0, spawnTransform.Count);
            Instantiate(spawnables[randomObj], spawnTransform[randomX].transform.position, spawnTransform[randomX].transform.rotation);
            timer = 0f;
        }
    }
}
