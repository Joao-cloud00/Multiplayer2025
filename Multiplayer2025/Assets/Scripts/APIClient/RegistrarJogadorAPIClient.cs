using System;
using System.Collections;
using UnityEngine;

public class RegistrarJogadorAPIClient : APIClient
{
    public RegistrarJogadorAPIClient(string baseUrl) : base(baseUrl) { }

    public IEnumerator RegistrarJogador(Jogador jogador, Action<string> onSuccess, Action<string> onFailure)
    {
        string jsonData = JsonUtility.ToJson(jogador);

        Debug.Log("JSON Enviado: " + jsonData);
        yield return SendRequest("/api/jogador/registrar", "POST", jsonData,
            onSuccess, onFailure);
    }
}
