
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
                    // Supondo que o retorno da API seja algo como { "mensagem": "Login bem-sucedido!", "jogadorId": 123 }
                    var loginResponse = JsonUtility.FromJson<LoginResponse>(response);
                    Jogador jogador = new Jogador { cod = loginResponse.jogadorId };

                    onSuccess?.Invoke(jogador);  // Chama a função de sucesso com o jogador
                }
                catch (Exception ex)
                {
                    onFailure?.Invoke("Erro ao deserializar a resposta: " + ex.Message);
                }
            },
            onFailure);
    }

    // Classe para deserializar a resposta do login
    [System.Serializable]
    public class LoginResponse
    {
        public string mensagem;
        public int jogadorId;  // Recebe o jogadorId da resposta
    }
}
