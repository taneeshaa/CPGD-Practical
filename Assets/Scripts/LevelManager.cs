using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private RocketSpawner spawner;
    private int currLevel = 0;
    private float timer = 0;

    public GameObject SignInObject;
    public UserProfile profile;

    public TextMeshProUGUI levelsText; 

    public DetectCollision levelData;
    ProfileData profileData;

    private bool loggedIn;
    void Start()
    {
        spawner = GetComponent<RocketSpawner>();
        profileData = profile.profileData;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 30 && !loggedIn)
        {
            currLevel++;
            levelsText.text = "Level: " + currLevel;
            SignInObject.SetActive(true);
            profile.enabled = true;
            spawner.enabled = false;

            //set user data
            //profileData.rocketCount = levelData.rocketCount;
            //profileData.gemCount = levelData.gemCount;
            //profileData.level = currLevel;
            //profile.SetUserData();
            loggedIn = true;
            timer = 0;
        }
    }
}
