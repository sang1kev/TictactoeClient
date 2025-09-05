using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkManager : Singleton<NetworkManager>
{
    public IEnumerator SignUp(SignUpData signUpData, Action success, Action<int> failure)
    {
        string jsonString = JsonUtility.ToJson(signUpData);
        byte[] byteRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest www = new UnityWebRequest(Constants.ServerURL + "/users/signup",
            UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(byteRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {

            }
            else
            {
                var resultString = www.downloadHandler.text;
                var result = JsonUtility.FromJson<SignUpResult>(resultString);

                if (result.result == 2)
                {
                    success?.Invoke();
                }
                else
                {
                    failure?.Invoke(result.result);
                }
            }
        }
        ;
    }

    public IEnumerator SignIn(SignInData signInData, Action success, Action<int> failure)
    {
        string jsonString = JsonUtility.ToJson(signInData);
        byte[] byteRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest www = new UnityWebRequest(Constants.ServerURL + "/users/signin",
            UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(byteRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {

            }
            else
            {
                var resultString = www.downloadHandler.text;
                var result = JsonUtility.FromJson<SignInResult>(resultString);

                if (result.result == 2)
                {
                    success?.Invoke();
                    GameManager.Instance.LogInStatus();
                }
                else
                {
                    failure?.Invoke(result.result);
                }
            }
        };
    }


    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {

    }
}
