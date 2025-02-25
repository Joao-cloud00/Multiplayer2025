using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private byte[] buffer = new byte[1024];

    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 5000); // Altere para o IP do servidor se necessário
            stream = client.GetStream();
            Debug.Log("Conectado ao servidor!");

            // Iniciar a leitura em uma thread separada para não travar a Unity
            Thread receiveThread = new Thread(ReceiveData);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.LogError("Erro ao conectar ao servidor: " + e.Message);
        }
    }

    void ReceiveData()
    {
        while (client.Connected)
        {
            try
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Debug.Log("Mensagem do servidor: " + message);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Erro ao receber dados: " + e.Message);
                break;
            }
        }
    }

    void SendMessageToServer(string message)
    {
        if (client == null || !client.Connected) return;

        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
        Debug.Log("Enviado ao servidor: " + message);
    }

    void OnApplicationQuit()
    {
        if (client != null)
        {
            client.Close();
        }
    }
}
