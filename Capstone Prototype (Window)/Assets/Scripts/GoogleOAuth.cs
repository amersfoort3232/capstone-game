/*

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

public class GoogleOAuth : MonoBehaviour
{
    private const string authorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
    private const string tokenEndpoint = "https://accounts.google.com/o/oauth2/token";
    private const string redirectURI = "http://localhost";

    public string clientID;

    public async void SignIn()
    {
        string authCode = await GetAuthorizationCode();

        if (authCode == null)
        {
            Debug.LogError("Authorization code is null");
            return;
        }

        string accessToken = await GetAccessToken(authCode);

        if (accessToken == null)
        {
            Debug.LogError("Access token is null");
            return;
        }

        string userProfile = await GetUserProfile(accessToken);

        if (userProfile == null)
        {
            Debug.LogError("User profile is null");
            return;
        }

        Debug.Log(userProfile);
    }
    private async Task<string> GetAuthorizationCode()
    {
        string authorizationRequest = string.Format(
            "{0}?response_type=code&client_id={1}&redirect_uri={2}&scope=https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile",
            authorizationEndpoint,
            clientID,
            redirectURI
        );

        string authorizationCode = null;

        UnityWebRequest webRequest = UnityWebRequest.Get(authorizationRequest);
        webRequest.useHttpContinue = false;

        UnityWebRequestAsyncOperation asyncOp = webRequest.SendWebRequest();

        while (!asyncOp.isDone)
        {
            await Task.Delay(100);
        }

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(webRequest.error);
        }
        else if (webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(webRequest.error);
        }
        else if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string responseText = webRequest.downloadHandler.text;
            Debug.Log(responseText);

            // Parse the authorization code from the redirect URI
            string[] responseParts = responseText.Split('?');
            string[] queryParams = responseParts[1].Split('&');

            foreach (string queryParam in queryParams)
            {
                string[] paramParts = queryParam.Split('=');

                if (paramParts[0] == "code")
                {
                    authorizationCode = paramParts[1];
                    break;
                }
            }
        }

        return authorizationCode;
    }
    private async Task<string> GetAccessToken(string authorizationCode)
    {
        string accessToken = null;

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("code", authorizationCode));
        formData.Add(new MultipartFormDataSection("client_id", clientID));
        formData.Add(new MultipartFormDataSection("redirect_uri", redirectURI));
        formData.Add(new MultipartFormDataSection("grant_type", "authorization_code"));

        UnityWebRequest webRequest = UnityWebRequest.Post(tokenEndpoint, formData);
        webRequest.useHttpContinue = false;

        UnityWebRequestAsyncOperation asyncOp = webRequest.SendWebRequest();

        while (!asyncOp.isDone)
        {
            await Task.Delay(100);
        }

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(webRequest.error);
        }
        else if (webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(webRequest.error);
        }
        else if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string responseText = webRequest.downloadHandler.text;
            Debug.Log(responseText);

            // Parse the access token from the JSON response
            var json = JSON.Parse(responseText);
            accessToken = json["access_token"];
        }

        return accessToken;
    }



}

*/
