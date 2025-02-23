using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIClient
{
    private readonly string _baseUrl;

    public APIClient(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    protected IEnumerator SendRequest(string url, string method, string jsonData, Action<string> onSuccess, Action<string> onFailure)
    {
        // URL completa com base URL do servidor
        using (UnityWebRequest request = new UnityWebRequest(_baseUrl + url, method))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);  // Usa Encoding para converter string para bytes
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                onFailure?.Invoke(request.error);
            }
            else
            {
                onSuccess?.Invoke(request.downloadHandler.text);
            }
        }
    }
}
