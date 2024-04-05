using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class UserAccountManager : MonoBehaviour
{
    public static UserAccountManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void CreateAccount(string username, string emailAddress, string password)
    {
        PlayFabClientAPI.RegisterPlayFabUser(
            new RegisterPlayFabUserRequest
            {
                Email = emailAddress,
                Password = password,
                Username = username,
                RequireBothUsernameAndEmail = true
            },
            response =>
            {
                Debug.Log($"successful account creation: {username}, {emailAddress}");
            },
            error =>
            {
                Debug.Log($"Unsuccessful account creation: {username}, {emailAddress} \n {error.ErrorMessage}");
            }
        );
    }
}
