using System;
using System.Collections;
using UnityEngine;

public class ListarJogadorAPIClient : APIClient
{
    public ListarJogadorAPIClient(string baseUrl) : base(baseUrl) { }

    public IEnumerator Login(string usuario, string senha, Action<Jogador> onSuccess, Action<string> onFailure)
    {
        string jsonData = JsonUtility.ToJson(new { Usuario = usuario, Senha = senha });
        yield return SendRequest("/api/jogador/listar", "POST", jsonData,
            (response) => {
                Jogador jogador = JsonUtility.FromJson<Jogador>(response);
                onSuccess?.Invoke(jogador);
            },
            onFailure);
    }
}

