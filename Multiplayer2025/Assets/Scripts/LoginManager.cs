using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public InputField[] inputNome;
    public InputField[] inputUsuario;
    public InputField[] inputSenha;
    public TextMeshProUGUI[] mensagem;
    public GameObject[] panels;
    public GameObject[] botoes;

    public void Registrar()
    {
        if (inputSenha[0].text == inputSenha[1].text)
        {
            Jogador novoJogador = new Jogador
            {
                nome = inputNome[0].text,
                usuario = inputUsuario[0].text,
                senha = inputSenha[0].text,
            };

            var registrarJogador = new RegistrarJogadorAPIClient("http://localhost:5213"); // URL do servidor
            StartCoroutine(registrarJogador.RegistrarJogador(novoJogador,
                jogador => Registrou(jogador),
                erro => NaoRegistrou(erro)
            ));

        }
        else { mensagem[0].text = "Senhas não coincidem"; }
    }
    void Registrou(string jogador)
    {
        mensagem[0].text = jogador;

    }

    void NaoRegistrou(string erro)
    {
        mensagem[0].text = erro;
        Debug.Log("Erro ao realizar cadastro: " + erro);
    }

    public void Logar()
    {
        var loginAPIClient = new LoginAPIClient("http://localhost:5213"); // URL do servidor

        string usuario = inputUsuario[1].text;
        string senha = inputSenha[2].text;

        StartCoroutine(loginAPIClient.Login(usuario, senha,
           jogador => Logou(jogador),
           erro => NaoLogou(erro)
       )); ;
    }
    void Logou(Jogador jogador)
    {
        mensagem[1].text = "Login bem-sucedido! Clique para Avançar";

        Debug.Log("Login bem-sucedido! Jogador Cod: " + jogador.cod);

        // Armazene o ID do jogador
        PlayerPrefs.SetInt("JogadorCod", jogador.cod);  // Armazena o ID do jogador
        Debug.Log(PlayerPrefs.GetInt("JogadorCod"));
        botoes[0].SetActive(false);
        botoes[1].SetActive(false);
        botoes[2].SetActive(true);
    }

    void NaoLogou(string erro)
    {
        mensagem[1].text = "Usuario ou senha incorretos";
        Debug.Log("Erro ao realizar login: " + erro);
    }
    public void IniciarLogar()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(false);
        panels[2].SetActive(true);
    }
    public void IniciarCadastro()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);
        panels[2].SetActive(false);
    }
    public void Voltar()
    {
        panels[0].SetActive(true);
        panels[1].SetActive(false);
        panels[2].SetActive(false);
    }
    public void Avançar()
    {
        SceneManager.LoadScene(1);
    }
}