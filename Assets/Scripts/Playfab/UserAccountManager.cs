using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;
using static UnityEditor.AddressableAssets.Settings.AddressableAssetProfileSettings;
using static UnityEditor.AddressableAssets.Build.Layout.BuildLayout;

public class UserAccountManager : MonoBehaviour
{
    public static UserAccountManager Instance;

    public static UnityEvent OnSignInSuccess = new UnityEvent();

    public static UnityEvent<string> OnSignInFailure = new UnityEvent<string>();
    
    public static UnityEvent<string> OnSignInFailed = new UnityEvent<string>();

    public static UnityEvent<string, string> OnUserDataRetrieved = new UnityEvent<string, string>();

    public DetectCollision levelData;
    ProfileData profileData;
    public UserProfile profile;

    string playfabID;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        profileData = profile.profileData;
    }
    public void CreateAccount(string username, string emailAddress, string password)
    {
        PlayFabClientAPI.RegisterPlayFabUser(
            new RegisterPlayFabUserRequest
            {
                //Email = emailAddress,
                Password = password,
                Username = username,
                //RequireBothUsernameAndEmail = true
                RequireBothUsernameAndEmail = false
            },
            response =>
            {
                Debug.Log($"successful account creation: {username}, {emailAddress}");
                SignIn(username, password);
            },
            error =>
            {
                Debug.Log($"Unsuccessful account creation: {username}, {emailAddress} \n {error.ErrorMessage} \n {error.ErrorDetails}");
                Debug.Log(error);
                OnSignInFailed.Invoke(error.ErrorMessage);
            }
        );
    }

    public void SignIn( string username, string password)
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest()
        {
            Username = username,
            Password = password
        },
        response =>
        {
            Debug.Log($"successful login: {username}");
            playfabID = response.PlayFabId;
            OnSignInSuccess.Invoke();
        },
        error =>
        {
            Debug.Log($"unsuccessful login: {username} \n {error.ErrorMessage}");
            OnSignInFailure.Invoke(error.ErrorMessage);
        });
    }

    void GetDeviceID (out string android_id, out string ios_id, out string custom_id)
    {
        android_id = string.Empty;
        ios_id = string.Empty;
        custom_id = string.Empty;

        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
            android_id = secure.CallStatic<string>("getString", contentResolver, "android_id");
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            ios_id = UnityEngine.iOS.Device.vendorIdentifier;
        }
        else
        {
            custom_id = SystemInfo.deviceUniqueIdentifier;
        }
    }

    public void SignInWithDevice()
    {
        GetDeviceID(out string android_id, out string ios_id, out string custom_id);
        if (!string.IsNullOrEmpty(android_id))
        {
            Debug.Log($"Logging in with Android Device ID");
            PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest() {
                AndroidDeviceId = android_id,
                    OS = SystemInfo.operatingSystem,
                    AndroidDevice = SystemInfo.deviceModel,
                    TitleId = PlayFabSettings.TitleId,
                    CreateAccount = true
            },
                response => {
                    Debug.Log($"Successful login with Android Device ID");
                    playfabID = response.PlayFabId;
                    OnSignInSuccess.Invoke();
                },
                error => {
                    Debug.Log($"Unsuccessful login with Android Device ID: {error.ErrorMessage}");
                    OnSignInFailed.Invoke(error.ErrorMessage);
                });
        } else if(!string.IsNullOrEmpty(ios_id)){
            Debug.Log($"Logging in with IOS Device ID");
            PlayFabClientAPI.LoginWithIOSDeviceID(new LoginWithIOSDeviceIDRequest()
            {
                DeviceId = ios_id,
                OS = SystemInfo.operatingSystem,
                DeviceModel = SystemInfo.deviceModel,
                TitleId = PlayFabSettings.TitleId,
                CreateAccount = true
            },
                response => {
                    Debug.Log($"Successful login with IOS Device ID");
                    playfabID = response.PlayFabId;
                    OnSignInSuccess.Invoke();
                },
                error => {
                    Debug.Log($"Unsuccessful login with IOS Device ID: {error.ErrorMessage}");
                    OnSignInFailed.Invoke(error.ErrorMessage);
                });
        } else if (!string.IsNullOrEmpty(custom_id))
        {
            Debug.Log($"Logging in with Custom Device ID");

            //set user data
            //profileData.rocketCount = levelData.rocketCount;
            //profileData.gemCount = levelData.gemCount;
            //profileData.level = 1;

            PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
            {
                CustomId = custom_id,
                TitleId = PlayFabSettings.TitleId,
                CreateAccount = true
            },
                response => {
                    Debug.Log($"Successful login with Custom Device ID");
                    playfabID = response.PlayFabId;
                    OnSignInSuccess.Invoke();
                },
                error => {
                    Debug.Log($"Unsuccessful login with Custom Device ID: {error.ErrorMessage}");
                    OnSignInFailed.Invoke(error.ErrorMessage);
                });
        }
    }

    public void GetUserData(string key)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = playfabID,
            Keys = new List<string>()
            {
                key
            }
        },
        response =>
        {
            Debug.Log("successful getuserdata");
            if (response.Data.ContainsKey(key)) OnUserDataRetrieved.Invoke(key, response.Data[key].Value);
            else OnUserDataRetrieved.Invoke(key, null);
        },
        error =>
        {
            Debug.Log($"unsuccessful getuserdata: {error.ErrorMessage}");
        }) ;
    }

    public void SetUserData(string key, string value)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                { key, value }
            }
        },
        response =>
        {
            Debug.Log("successful setuserdata");
        },
        error =>
        {
            Debug.Log($"unsuccessful setuserdata: {error.ErrorMessage}");
        });
    }
}
