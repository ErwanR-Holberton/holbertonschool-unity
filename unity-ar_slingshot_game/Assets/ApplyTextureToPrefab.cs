using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApplyTextureToPrefab : MonoBehaviour
{
    public string url1 = "https://erhbtn.pythonanywhere.com/Redirect/get_url";
    public Material targetMaterial;  // Reference to the shared material used by the prefab
    
    void Start()
    {
        StartCoroutine(RequestUrl1());
    }

    // Coroutine to request URL 1
    IEnumerator RequestUrl1()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url1);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogError("Error requesting URL 1: " + webRequest.error);
        else
        {
            string url2 = webRequest.downloadHandler.text.Trim();
            Debug.Log("URL 2 received: " + url2);
            StartCoroutine(RequestImage(url2));
        }
    }

    // Coroutine to request an image from URL 2
    IEnumerator RequestImage(string imageUrl)
    {
        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl);

        yield return imageRequest.SendWebRequest();

        if (imageRequest.result == UnityWebRequest.Result.ConnectionError || imageRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error requesting image from URL 2: " + imageRequest.error);
        }
        else
        {
            // Get the texture from the response
            Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(imageRequest);

            // Apply the texture to the target material
            if (targetMaterial != null)
            {
                targetMaterial.mainTexture = downloadedTexture;
                Debug.Log("Image applied to material successfully!");
            }
            else
            {
                Debug.LogError("Target material is not assigned.");
            }
        }
    }
}
