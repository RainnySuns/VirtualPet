using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Text;

using UnityEngine.UI;


public class SendTextData : MonoBehaviour
{
    public InputField inputField;
    private string inputpath = "Assets/Samples/OpenAI Unity/0.2.0/Vpet Scenes/TestScene/InputPrompt";
    private bool isdeliver = true;
    private string time;
    private string filepath;
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    void Start()
    {
        //StartCoroutine(PostTextData("http://192.168.0.114:5002/generate", "jump while dancing"));
        //Debug.Log("Success");
        //Debug.Log(json);
    }

    void Update()
    {

    }

    string CreateJsonWithPrompt(string text)
    {
        Dictionary<string, string> dataObject = new Dictionary<string, string>();
        dataObject.Add("text_prompt", text);
        dataObject.Add("timestamp", DateTime.Now.ToString("MMddHHmmss"));
        time = DateTime.Now.ToString("MMddHHmmss");
        return JsonConvert.SerializeObject(dataObject);
    }

    IEnumerator PostTextData(string url, string textData)
    {
        string jsonData = CreateJsonWithPrompt(textData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");


        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }

    public void SendText()
    {
        Debug.Log("Clicked!");
        StartCoroutine(PostTextData("http://147.8.92.231:5002/generate", inputField.text));
        if (inputField != null)
        {
            inputField.text = string.Empty;
        }
        else
        {
            Debug.LogError("InputField is not assigned.");
        }

        //if (isdeliver)
        //{
            StartCoroutine(DownloadFile("http://147.8.92.231:5003/download?timestamp=" + time));
        //}
    }

    public void DeliverText(string text)
    {
        StartCoroutine(PostTextData("http://147.8.92.231:5002/generate", text));
                
        StartCoroutine(DownloadFile("http://147.8.92.231:5003/download?timestamp=" + time));
    }

    IEnumerator DownloadFile(string uri)
    {
        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string contentDisposition = request.GetResponseHeader("Content-Disposition");
            string filename = new System.Net.Mime.ContentDisposition(contentDisposition).FileName;

            if (string.IsNullOrEmpty(filename))
            {
                filename = "default_filename.bvh";
            }

            string path = "Assets/Samples/OpenAI Unity/0.2.0/Vpet Scenes/TestScene/BVHFile/" + filename;
            try
            {
                System.IO.File.WriteAllBytes(path, request.downloadHandler.data);
                Debug.Log("File successfully downloaded and saved to BVHFile/" + filename);
                filepath = path;
                if (BVHDriver.Instance == null)
                {
                    Debug.LogError("BVHDriver instance is not initialized.");
                }
                else
                {
                    BVHDriver.Instance.Filename = filepath;
                    Debug.Log("filename has changed");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to write file: " + e.Message);
            }
        }
    }
}
