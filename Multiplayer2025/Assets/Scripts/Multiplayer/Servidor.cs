using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Servidor : MonoBehaviour
{
    private TcpListener server;
    private TcpClient player1;
    private TcpClient player2;

    // Estrutura de mensagem para facilitar o controle de mensagens
    public class GameMessage
    {
        public string Type { get; set; }
        public string Content { get; set; }
    }

    void Start()
    {
        server = new TcpListener(IPAddress.Any, 5000);
        server.Start();
        Debug.Log("Server started on port 5000");
        server.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), null);
    }

    void OnClientConnect(IAsyncResult ar)
    {
        TcpClient client = server.EndAcceptTcpClient(ar);
        Debug.Log("Client connected");

        // Atribuindo jogadores
        if (player1 == null)
        {
            player1 = client;
            Debug.Log("Player 1 connected");
        }
        else if (player2 == null)
        {
            player2 = client;
            Debug.Log("Player 2 connected");
            StartGame();
        }

        // Continuar aceitando mais clientes
        server.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), null);

        // Iniciar a leitura de dados do cliente
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnDataReceived), Tuple.Create(client, buffer));
    }

    private void StartGame()
    {
        if (player1 != null && player2 != null)
        {
            Debug.Log("StartGame");
            SendMessageToClient(player1,"teste","teste");
            int coinFlip = UnityEngine.Random.Range(0, 2);

            if (coinFlip == 0)
            {
                SendMessageToClient(player1, "StartTurn", "0");
                SendMessageToClient(player2, "StartTurn", "1");
            }
            else
            {
                SendMessageToClient(player1, "StartTurn", "1");
                SendMessageToClient(player2, "StartTurn", "0");
            }
        }
        else
        {
            Debug.LogWarning("Aguardando dois jogadores para iniciar o jogo.");
        }
    }

    void OnDataReceived(IAsyncResult ar)
    {
        var state = (Tuple<TcpClient, byte[]>)ar.AsyncState;
        TcpClient client = state.Item1;
        byte[] buffer = state.Item2;

        NetworkStream stream = client.GetStream();
        int bytesRead = stream.EndRead(ar);

        if (bytesRead > 0)
        {
            string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Debug.Log("Received message: " + message);

            // Processar a mensagem manualmente, verificando o tipo
            if (message.StartsWith("{") && message.EndsWith("}"))
            {
                // Simulando a estrutura de um JSON simples
                string type = GetJsonValue(message, "Type");
                string content = GetJsonValue(message, "Content");

                // Lógica para responder às mensagens de acordo com o tipo
                if (type == "Move")
                {
                    Debug.Log($"Player move: {content}");

                    // Enviar a mensagem para o outro jogador
                    if (client == player1 && player2 != null)
                    {
                        SendMessageToClient(player2, "Move", content);
                    }
                    else if (client == player2 && player1 != null)
                    {
                        SendMessageToClient(player1, "Move", content);
                    }
                }
            }

            // Continuar lendo dados do cliente
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnDataReceived), Tuple.Create(client, buffer));
        }
        else
        {
            // Cliente desconectado
            Debug.Log("Player disconnected.");
            if (client == player1)
                player1 = null;
            else if (client == player2)
                player2 = null;
        }
    }

    void SendMessageToClient(TcpClient client, string type, string content)
    {
        if (client == null) return;

        // Criar uma mensagem simples no formato de "JSON" manual
        string jsonMessage = $"{{\"Type\":\"{type}\",\"Content\":\"{content}\"}}";

        NetworkStream stream = client.GetStream();
        byte[] responseData = Encoding.ASCII.GetBytes(jsonMessage);
        stream.Write(responseData, 0, responseData.Length);
    }

    // Função para extrair o valor de um campo JSON simples
    string GetJsonValue(string json, string key)
    {
        string search = $"\"{key}\":\"";
        int startIndex = json.IndexOf(search) + search.Length;
        int endIndex = json.IndexOf("\"", startIndex);
        return json.Substring(startIndex, endIndex - startIndex);
    }

    void OnApplicationQuit()
    {
        // Fechar as conexões dos jogadores
        if (player1 != null)
        {
            player1.Close();
            Debug.Log("Player 1 disconnected.");
        }

        if (player2 != null)
        {
            player2.Close();
            Debug.Log("Player 2 disconnected.");
        }

        // Parar o servidor
        server.Stop();
        Debug.Log("Server stopped.");
    }
}
