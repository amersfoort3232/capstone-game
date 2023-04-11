using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using JsonData;

public class DialogflowAPIScript : MonoBehaviour {

	void Start () {
		// AccessToken is being generated manually in terminal
		StartCoroutine(PostRequest("https://dialogflow.googleapis.com/v2/projects/capstone-379918/agent/sessions/1234:detectIntent",
                              "ya29.c.b0Aaekm1JWOKn7NKJDdajC3a97xhxPYBakRre_7NSQmNIJGYWykYtdX2Sc2VbRC0hLzRf6k86GJ1TJKXM-WX0xjY-LMwRA_CJe_bo-59YTcAjT4vSz7ahfzed1peZnmSqxGwBKnaz-apkERGsV7ImkLVao185cIGfQtdBQIgpLrfarUW6oGy9BSbe4wLpShpYmNeucoTyP41jTZj1IBFdYKRrEIz1mlToCymH1L1uZCMoUca9B8fxXn5riKn46NG244SMUTb0iGUuTQWwhBvGUOfu-4sBSXIi-jiUlma1ELZhdPDBY1VDQbeU00jfxbZp5M5QjxSDWydML340DMclY11O4FYs5_kSpaIQfbsj-Wa3X5nUdcn_8o_wdoV644zS5whYxymzjwnMJbafX00_69eex_npuu9n2MVzt1rXchVS0amyVnrc9eUwZFtfjhlj77U-RU9lM2asb4MhR5Y0uX1ZzIa68Rlhy6WMOUZp0UWnW1q8x5Rd0dSygkfrOBvMFyUql8uzWXZ63an5UjmljWScb-trQXwglg_og1ljguS49ixR2brqznYf9cuvgxU-uetJIy2dFBbjFrMhJ0sukbfMVw1tVUM1JBlVSXeV-J0yvyXBj5fRUnh--z-UI5h8Ya6ih9qIIwIFewB8_cdFh5aUvkzcXgQh0adQJ0yQ66i_UvQolfowIpkV0c0X5r3bSR65Ug1ZXcyiIWaVUn97nFR4lwYtil9JMwWOwzb028Rpb-eBRJOUWjwonggcrr-ov-5ucFeM2OcZpZ8romY-z41ou5UyheMVgkq35oc9xft9W7phWhv4vcq0OWJtFW7o8Ogomy3eFSjwWqmw_JjvB97ZtfOphXuurR7WuOzUnW4yt1U0zYFV5ey2pvgxQS_-6nObjtZ3ofpkhRpvkYFm3khgJa8II4tZsb3JVje3BVhWr4jbeR2of42p3JbBf-zUzQYk_lZnbtck4vz-JzV6BS1S__dtcwv3_gu4v1Wxio-aJo2dXmh3nkwB"));
	}

	IEnumerator PostRequest(String url, String AccessToken){
		UnityWebRequest postRequest = new UnityWebRequest(url, "POST");
		RequestBody requestBody = new RequestBody();
		requestBody.queryInput = new QueryInput();

		string jsonRequestBody = JsonUtility.ToJson(requestBody,true);
		Debug.Log(jsonRequestBody);

		byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
		postRequest.SetRequestHeader("Authorization", "Bearer " + AccessToken);
		postRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
		postRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

		yield return postRequest.SendWebRequest();

		if(postRequest.isNetworkError || postRequest.isHttpError)
		{
			Debug.Log(postRequest.responseCode);
			Debug.Log(postRequest.error);
		}
		else
		{
			// Show results as text
			Debug.Log("Response: " + postRequest.downloadHandler.text);

			// Or retrieve results as binary data
			byte[] resultbyte = postRequest.downloadHandler.data;
			string result = System.Text.Encoding.UTF8.GetString(resultbyte);
			ResponseBody content = (ResponseBody)JsonUtility.FromJson<ResponseBody>(result);
			Debug.Log(content.queryResult.fulfillmentText);

		}
	}

	IEnumerator GetAgent(String AccessToken)
	{
		UnityWebRequest www = UnityWebRequest.Get("https://dialogflow.googleapis.com/v2/projects/capstone-379918/agent");

		www.SetRequestHeader("Authorization", "Bearer " + AccessToken);

		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.error);
		}
		else
		{
			// Show results as text
			Debug.Log(www.downloadHandler.text);

			// Or retrieve results as binary data
			byte[] results = www.downloadHandler.data;
		}
	}
}