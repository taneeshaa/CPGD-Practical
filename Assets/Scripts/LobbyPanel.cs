using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;


public class LobbyPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private async void Initialize()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("unity services initialized");
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log($"Player authenticated {AuthenticationService.Instance.PlayerId}");
    }

    public async void CreateLobby()
    {
        Lobby myLobby = await LobbyService.Instance.CreateLobbyAsync("MyLobby", 4);
        Debug.Log($"Create lobby success {myLobby.Id}, {myLobby.LobbyCode}, {myLobby.Name}");
    }

    public async void JoinLobby()
    {
        try
        {
            Lobby myLobby = await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch(LobbyServiceException e)
        {
            Debug.Log($"unable to join a lobby: {e.Reason}, {e.Message}. {e.ErrorCode}");
        }
    }

    public async void JoinLobbyByCode(TMPro.TMP_InputField codefield)
    {
        try
        {
            Lobby myLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(codefield.text);
            Debug.Log($"Join lobby success {myLobby.Id}, {myLobby.LobbyCode}, {myLobby.Name}");
        }
        catch(LobbyServiceException e)
        {
            Debug.Log($"unable to join a lobby: {e.Reason}, {e.Message}. {e.ErrorCode}");
        }
    }
}
