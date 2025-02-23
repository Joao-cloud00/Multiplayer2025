using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    public static _SceneManager Instance;

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
