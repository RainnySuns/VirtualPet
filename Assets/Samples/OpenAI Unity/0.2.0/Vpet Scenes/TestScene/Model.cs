using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Model : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadFile("http://cave01@192.108.0.114/getfile"));
    }

    // Update is called once per frame
    void Update()
    {
        
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

            System.IO.File.WriteAllBytes("BVHFile/" + filename, request.downloadHandler.data);
            Debug.Log("File successfully downloaded and saved to BVHFile/" + filename);
        }
    }
}
