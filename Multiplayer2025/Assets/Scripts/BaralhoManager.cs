using UnityEngine;
using System.Collections.Generic;

public class BaralhoManager : MonoBehaviour
{
    //void Start()
    //{
    //    // Recupera os baralhos armazenados
    //    BaralhoManager baralhoManager = FindObjectOfType<BaralhoManager>();
    //    List<string> baralho1 = baralhoManager.RecuperarBaralho(1);
    //    List<string> baralho2 = baralhoManager.RecuperarBaralho(2);

    //    // Exibe as cartas do baralho
    //    MostrarCartas(baralho1, baralho2);
    //}

    private List<string> baralho1;
    private List<string> baralho2;

    void Start()
    {
        // Recupera os baralhos do PlayerPrefs
        string baralho1Json = PlayerPrefs.GetString("Baralho1", "{}");
        string baralho2Json = PlayerPrefs.GetString("Baralho2", "{}");

        // Converte de JSON para lista usando a classe wrapper
        baralho1 = JsonUtility.FromJson<BaralhoWrapper>(baralho1Json)?.cartas ?? new List<string>();
        baralho2 = JsonUtility.FromJson<BaralhoWrapper>(baralho2Json)?.cartas ?? new List<string>();

        // Teste para ver se os baralhos foram carregados corretamente
        Debug.Log("Baralho 1: " + string.Join(", ", baralho1));
        Debug.Log("Baralho 2: " + string.Join(", ", baralho2));
    }

    [System.Serializable]
    private class BaralhoWrapper
    {
        public List<string> cartas;
    }

    private void MostrarCartas(List<string> baralho1, List<string> baralho2)
    {
        // Use seu código para mostrar as cartas no UI, por exemplo
        Debug.Log("Baralho 1: " + string.Join(", ", baralho1));
        Debug.Log("Baralho 2: " + string.Join(", ", baralho2));
    }
    public void ArmazenarBaralho(List<string> nomesCartasBaralho1, List<string> nomesCartasBaralho2)
    {
        // Armazena os nomes das cartas do baralho 1, incluindo cópias corretamente
        PlayerPrefs.SetInt("Baralho1_Count", nomesCartasBaralho1.Count);
        for (int i = 0; i < nomesCartasBaralho1.Count; i++)
        {
            PlayerPrefs.SetString("Baralho1_Carta_" + i, nomesCartasBaralho1[i]); // Agora cada carta tem um índice único
        }

        // Armazena os nomes das cartas do baralho 2, incluindo cópias corretamente
        PlayerPrefs.SetInt("Baralho2_Count", nomesCartasBaralho2.Count);
        for (int i = 0; i < nomesCartasBaralho2.Count; i++)
        {
            PlayerPrefs.SetString("Baralho2_Carta_" + i, nomesCartasBaralho2[i]); // Cada carta tem um índice único
        }

        // Salva as alterações
        PlayerPrefs.Save();
    }


    public List<string> RecuperarBaralho(int baralhoIndex)
    {
        List<string> nomesCartas = new List<string>();

        int count = PlayerPrefs.GetInt("Baralho" + baralhoIndex + "_Count", 0);
        for (int i = 0; i < count; i++)
        {
            string carta = PlayerPrefs.GetString("Baralho" + baralhoIndex + "_Carta_" + i, ""); // Recupera a carta pelo índice
            if (!string.IsNullOrEmpty(carta))
            {
                nomesCartas.Add(carta); // Adiciona cada carta, mesmo se for repetida
            }
        }

        return nomesCartas;
    }

}
