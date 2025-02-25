using UnityEngine;
using System;
using System.Collections.Generic;
using static Unity.Burst.Intrinsics.X86.Avx;
using System.Collections;

public class PartidaAPIClient : APIClient
{
    public PartidaAPIClient(string baseUrl) : base(baseUrl) { }
    public IEnumerator RegistrarPartida(int baralho1, int baralho2, Action<Jogador> onSuccess, Action<string> onFailure)
    {
        // Criar a URL com os parâmetros de query
        string url = $"//api/Partida/registrar?baralho_01={baralho1}&baralho_02={baralho2}";

        yield return SendRequest(url, "POST", "", // Enviando uma requisição POST sem corpo
            (response) => {
                // Quando a resposta for recebida com sucesso
                // A resposta deve ser convertida para um objeto Jogador
                try
                {
                    // Supondo que o retorno da API seja algo como { "mensagem": "Login bem-sucedido!", "jogadorId": 123 }
                    var RegistrarPartidaResponse = JsonUtility.FromJson<LoginResponse>(response);
                    Jogador jogador = new Jogador { cod = RegistrarPartidaResponse.jogadorId };

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

    //public Partida CreatePartida(Partida partida)
    //{
    //    string sql = "INSERT INTO PARTIDA (BARALHO_01, BARALHO_02) " +
    //                 "VALUES (@BARALHO_01, @BARALHO_02);" +
    //    "SELECT SCOPE_IDENTITY() AS COD;";

    //    cmd = new SqlCommand(sql, conectar());
    //    par = new SqlParameter("@BARALHO_01", partida.baralho_01);
    //    par.SqlDbType = System.Data.SqlDbType.Int;
    //    cmd.Parameters.Add(par);
    //    par = new SqlParameter("@BARALHO_02", partida.baralho_02);
    //    par.SqlDbType = System.Data.SqlDbType.Int;
    //    cmd.Parameters.Add(par);

    //    reader = cmd.ExecuteReader();

    //    if (reader.Read())
    //    {
    //        partida.cod = Int32.Parse(reader.GetValue(reader.GetOrdinal("COD")).ToString());
    //        return partida;
    //    }
    //    return null;

    //}
}