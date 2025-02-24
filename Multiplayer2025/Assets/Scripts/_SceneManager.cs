using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class _SceneManager : MonoBehaviour
{
    public static _SceneManager Instance;
    public GameObject _panelMenu;
    public GameObject _panelCadastrar;
    public GameObject _panelLogin;
    public GameObject _btAvançar;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CarregarCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    // Carregar cena pelo índice (posição no Build Settings)
    public void CarregarCenaPorIndice(int indice)
    {
        SceneManager.LoadScene(indice);
    }

    // Recarregar a cena atual
    public void RecarregarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Sair do jogo
    public void SairDoJogo()
    {
        Application.Quit();
        Debug.Log("Jogo fechado!");
    }

    //------------------ buttons menu -------------------//
    public void AbrirCadastro()
    {
        _panelMenu.SetActive(false);
        _panelCadastrar.SetActive(true);
    }
    public void AbrirLogin()
    {
        _panelMenu.SetActive(false);
        _panelLogin.SetActive(true);
    }
    public void Fechar_cadastro()
    {
        _panelCadastrar.SetActive(false);
        _panelMenu.SetActive(true);
        
    }
    public void Fechar_login()
    {
        _panelLogin.SetActive(false);
        _panelMenu.SetActive(true);

    }
    public void Bt_confirmar()
    {
        _btAvançar.SetActive(true);
    }

    public void SelecionarDeck()
    {
        SceneManager.LoadScene(1);
    }
}
