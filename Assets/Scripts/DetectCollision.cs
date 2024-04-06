using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectCollision : MonoBehaviour
{
    public int gemCount = 0;
    public int rocketCount = 0;
    public int lives = 5;
    [SerializeField] RocketSpawner spawner;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI rocketText;
   

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
        if (other.gameObject.CompareTag("Rocket"))
        {            
            rocketCount++;
            rocketText.text = "Rockets: " + rocketCount.ToString();
            Destroy(other.gameObject);
            lives--;
            livesText.text = "Lives: " + lives;
        }
        if(other.gameObject.CompareTag("Gem"))
        {
            gemCount++;
            gemText.text = "Gems: " + gemCount.ToString();
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

