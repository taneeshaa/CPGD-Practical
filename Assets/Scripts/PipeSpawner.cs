using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnInterval = 2f;  // Adjust as needed
    public float spawnHeight = 3f;    // Adjust as needed
    public float pipeSpeed = 2f;      // Adjust as needed

    private void Start()
    {
        InvokeRepeating("SpawnPipe", 0f, spawnInterval);
    }

    private void SpawnPipe()
    {
        float randomY = Random.Range(-2f, 2f);  // Adjust as needed

        GameObject pipe = Instantiate(pipePrefab, new Vector3(transform.position.x, randomY, 0f), Quaternion.identity);
        //(pipe, 10f);  // Adjust as needed for the pipe to be destroyed after a certain time

        //Rigidbody2D pipeRb = pipe.GetComponent<Rigidbody2D>();
        //pipeRb.velocity = new Vector2(-pipeSpeed, pipeRb.velocity.y);
    }
}


