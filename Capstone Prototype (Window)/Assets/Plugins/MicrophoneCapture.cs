using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JsonData;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class MicrophoneCapture : MonoBehaviour
{

	//A boolean that flags whether there's a connected microphone
	private bool micConnected = false;

	//The maximum and minimum available recording frequencies
	private int minFreq;
	private int maxFreq;

	//A handle to the attached AudioSource
	private AudioSource goAudioSource;

	//Public variable for saving recorded sound clip
	public AudioClip recordedClip;
	private float[] samples;
	private byte[] bytes;

	private AudioSource audioSource;
	private readonly object thisLock = new object();
	private volatile bool recordingActive;

	private bool startButtonPressed = false;
	private bool stopButtonPressed = false;

	public UnityEngine.UI.Text ResponseText;

	public void StartButtonPressed()
	{
		startButtonPressed = true;
		stopButtonPressed = false;
	}

	public void StopButtonPressed()
	{
		startButtonPressed = false;
		stopButtonPressed = true;
	}

	void Start()
	{

		//Check microphone connected
		if (Microphone.devices.Length <= 0)
		{
			//warning message
			Debug.LogWarning("Microphone not connected!");
		}
		else
		{
			micConnected = true;

			Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

			if (minFreq == 0 && maxFreq == 0)
			{
				maxFreq = 44100;
			}

			goAudioSource = this.GetComponent<AudioSource>();
		}

	}

	void OnGUI()
	{
		//If there is a microphone
		if (micConnected)
		{
			//If the audio from any microphone isn't being captured
			if (!Microphone.IsRecording(null))
			{
				//Case the 'Start' button gets pressed
				if (startButtonPressed)
				{
					StartListening(goAudioSource);
				}
			}
			else //Recording
			{
				//Case the 'Stop' button gets pressed
				if (stopButtonPressed)
				{
					StopListening();
				}

				GUI.Label(new Rect(Screen.width / 5 - 100, Screen.height / 5 + 25, 200, 50), "Recording in progress...");
			}
		}
		else // No microphone
		{
			//Print a red "Microphone not connected!" message at the center of the screen
			GUI.contentColor = Color.red;
			GUI.Label(new Rect(Screen.width / 5 - 100, Screen.height / 5 - 25, 200, 50), "Microphone not connected!");
		}

	}

	public void StartListening(AudioSource audioSource)
	{
		lock (thisLock)
		{
			if (!recordingActive)
			{
				this.audioSource = audioSource;
				StartRecording();
			}
			else
			{
				Debug.LogWarning("Can't start new recording session while another recording session active");
			}
		}
	}

	public void StartRecording()
	{
		audioSource.clip = Microphone.Start(null, true, 20, 16000);
		recordingActive = true;
	}

	public void StopListening()
	{
		if (recordingActive)
		{
			lock (thisLock)
			{
				if (recordingActive)
				{
					StopRecording();
					bytes = WavUtility.FromAudioClip(audioSource.clip);
					audioSource.Play();
					Debug.Log("This is the audiosource clip length: "+ bytes.Length);
					audioSource = null;
				}
			}

			StartCoroutine(StartVoiceRequest("https://dialogflow.googleapis.com/v2/projects/capstone-379918/agent/sessions/1234:detectIntent",
				"ya29.c.b0Aaekm1JWOKn7NKJDdajC3a97xhxPYBakRre_7NSQmNIJGYWykYtdX2Sc2VbRC0hLzRf6k86GJ1TJKXM-WX0xjY-LMwRA_CJe_bo-59YTcAjT4vSz7ahfzed1peZnmSqxGwBKnaz-apkERGsV7ImkLVao185cIGfQtdBQIgpLrfarUW6oGy9BSbe4wLpShpYmNeucoTyP41jTZj1IBFdYKRrEIz1mlToCymH1L1uZCMoUca9B8fxXn5riKn46NG244SMUTb0iGUuTQWwhBvGUOfu-4sBSXIi-jiUlma1ELZhdPDBY1VDQbeU00jfxbZp5M5QjxSDWydML340DMclY11O4FYs5_kSpaIQfbsj-Wa3X5nUdcn_8o_wdoV644zS5whYxymzjwnMJbafX00_69eex_npuu9n2MVzt1rXchVS0amyVnrc9eUwZFtfjhlj77U-RU9lM2asb4MhR5Y0uX1ZzIa68Rlhy6WMOUZp0UWnW1q8x5Rd0dSygkfrOBvMFyUql8uzWXZ63an5UjmljWScb-trQXwglg_og1ljguS49ixR2brqznYf9cuvgxU-uetJIy2dFBbjFrMhJ0sukbfMVw1tVUM1JBlVSXeV-J0yvyXBj5fRUnh--z-UI5h8Ya6ih9qIIwIFewB8_cdFh5aUvkzcXgQh0adQJ0yQ66i_UvQolfowIpkV0c0X5r3bSR65Ug1ZXcyiIWaVUn97nFR4lwYtil9JMwWOwzb028Rpb-eBRJOUWjwonggcrr-ov-5ucFeM2OcZpZ8romY-z41ou5UyheMVgkq35oc9xft9W7phWhv4vcq0OWJtFW7o8Ogomy3eFSjwWqmw_JjvB97ZtfOphXuurR7WuOzUnW4yt1U0zYFV5ey2pvgxQS_-6nObjtZ3ofpkhRpvkYFm3khgJa8II4tZsb3JVje3BVhWr4jbeR2of42p3JbBf-zUzQYk_lZnbtck4vz-JzV6BS1S__dtcwv3_gu4v1Wxio-aJo2dXmh3nkwB",
				bytes));
		}
	}

	private void StopRecording()
	{
		Microphone.End(null);
		recordingActive = false;
	}

	IEnumerator StartVoiceRequest(String url, String AccessToken, object parameter)
	{
		byte[] samples = (byte[])parameter;

		string sampleString = System.Convert.ToBase64String(samples);
		if (samples != null)
		{
			UnityWebRequest postRequest = new UnityWebRequest(url, "POST");
			RequestBody requestBody = new RequestBody();
			requestBody.queryInput = new QueryInput();
			requestBody.queryInput.audioConfig = new InputAudioConfig();
			requestBody.queryInput.audioConfig.audioEncoding = AudioEncoding.AUDIO_ENCODING_LINEAR_16;

			requestBody.queryInput.audioConfig.sampleRateHertz = 16000;
			requestBody.queryInput.audioConfig.languageCode = "zh-hk";
			requestBody.inputAudio = sampleString;
			

			string jsonRequestBody = JsonUtility.ToJson(requestBody, true);
			Debug.Log(jsonRequestBody);

			byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
			postRequest.SetRequestHeader("Authorization", "Bearer " + AccessToken);
			postRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
			postRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

			yield return postRequest.SendWebRequest();

			if (postRequest.isNetworkError || postRequest.isHttpError)
			{
				Debug.Log(postRequest.responseCode);
				Debug.Log(postRequest.error);
			}
			else
			{

				Debug.Log("Response: " + postRequest.downloadHandler.text);

				// Or retrieve results as binary data
				byte[] resultbyte = postRequest.downloadHandler.data;
				
				string result = System.Text.Encoding.UTF8.GetString(resultbyte);
				ResponseBody content = (ResponseBody)JsonUtility.FromJson<ResponseBody>(result);
				Debug.Log(content.queryResult.fulfillmentText);

				ResponseText.text = "Response: "+ content.queryResult.fulfillmentText;

			}
		}else{
			Debug.LogError("The audio file is null");
		}
	}

	private void StartVoiceRequest(object parameter)
	{
		float[] samples = (float[])parameter;
		if (samples != null)
		{

		}
	}
}