using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectCollision : MonoBehaviour
{
    public int gemCount = 0;
    public int rocketCount = 0;
    public int lives = 5;
    public RocketSpawner spawner;

    // Update is called once per frame
    void Update()
    {
        if(lives <= 0)
        {
            RestartLevel();
            gemCount = 0;
            rocketCount = 0;
            lives = 5;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if (other.gameObject.CompareTag("Rocket"))
        {
            Debug.Log("Rocket encountered");
            rocketCount++;
            lives--;
        }
        if(other.gameObject.CompareTag("Gem"))
        {
            gemCount++;
            Destroy(other.gameObject);
            Debug.Log("Gem Count: "+ gemCount);
        }
    }
    void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

