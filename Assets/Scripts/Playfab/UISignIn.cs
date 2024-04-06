using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISignIn : MonoBehaviour
{
    [SerializeField] Text errorText;
    [SerializeField] Canvas canvas;

    string username, password;
    private void OnEnable()
    {
        UserAccountManager.OnSignInFailure.AddListener(OnSignInFailure);
        UserAccountManager.OnSignInSuccess.AddListener(OnSignInSuccess);
    }
    private void OnDisable()
    {
        UserAccountManager.OnSignInFailed.RemoveListener(OnSignInFailure); 
        UserAccountManager.OnSignInSuccess.RemoveListener(OnSignInSuccess);
    }
    void OnSignInFailure(string error)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = error;
    }
    void OnSignInSuccess()
    {
        canvas.enabled = false;
    }
    public void UpdateUsername(string _username)
    {
        username = _username;
    }
    public void UpdatePassword(string _password)
    {
        password = _password;
    }

    public void SignIn()
    {
        UserAccountManager.Instance.SignIn(username, password);
    }
}
