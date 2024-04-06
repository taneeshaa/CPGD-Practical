using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserProfile : MonoBehaviour
{
    [SerializeField] public DetectCollision detectCollision;
    [SerializeField] public ProfileData profileData;

    private void OnEnable()
    {
        UserAccountManager.OnSignInSuccess.AddListener(SignIn);
        UserAccountManager.OnUserDataRetrieved.AddListener(UserDataRetrieved);
    }
    private void OnDisable()
    {
        UserAccountManager.OnSignInSuccess.RemoveListener(SignIn);
        UserAccountManager.OnUserDataRetrieved.RemoveListener(UserDataRetrieved);
    }
    void SignIn()
    {
        GetUserData();
    }
    [ContextMenu("Get Profile Data")]
    public void GetUserData()
    {
        UserAccountManager.Instance.GetUserData("ProfileData");
    }
    public void UserDataRetrieved(string key, string value)
    {
        profileData = JsonUtility.FromJson<ProfileData>(value);
    }

    [ContextMenu("Set Profile Data")]
    public void SetUserData()
    {
        UserAccountManager.Instance.SetUserData("ProfileData", JsonUtility.ToJson(detectCollision));
    }
}

[System.Serializable]
public class ProfileData
{
    public string playerName;
    public float level;
    public int gemCount;
    public int rocketCount;
}


