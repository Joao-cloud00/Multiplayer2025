using System;
using System.Collections;
using UnityEngine;

public class LoginAPIClient : APIClient
{
    public LoginAPIClient(string baseUrl) : base(baseUrl) { }

    public IEnumerator Login(string usuario, string senha, Action<Jogador> onSuccess, Action<string> onFailure)
    {
        // Criar a URL com os parâmetros de query
        string url = $"/api/jogador/login?usuario={usuario}&senha={senha}";

        yield return SendRequest(url, "POST", "", // Enviando uma requisição POST sem corpo
            (response) => {
                // Quando a resposta for recebida com sucesso
                // A resposta deve ser convertida para um objeto Jogador
                try
                {
                    var jogador = JsonUtility.FromJson<Jogador>(response);
                    onSuccess?.Invoke(jogador);
                }
                catch (Exception ex)
                {
                    onFailure?.Invoke("Erro ao deserializar a resposta: " + ex.Message);
                }
            },
            onFailure);
    }
}
