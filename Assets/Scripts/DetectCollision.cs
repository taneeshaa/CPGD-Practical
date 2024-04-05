using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectCollision : MonoBehaviour
{
    public int gemCount = 0; 
    public RocketSpawner spawner;

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if (other.gameObject.CompareTag("Rocket"))
        {
            Debug.Log("Rocket encountered");
            spawner.enabled = false;
            Destroy(gameObject);
            gemCount = 0;
            RestartLevel();
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

