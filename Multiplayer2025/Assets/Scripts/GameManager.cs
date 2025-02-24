using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerScore = 0;    // Pontuação do jogador
    public int opponentScore = 0; // Pontuação do adversário
    public int maxScore = 3;      // Pontuação necessária para vencer

    [Header("UI Elements")]
    public TextMeshProUGUI playerScoreText;    // Texto para mostrar a pontuação do jogador
    public TextMeshProUGUI opponentScoreText;  // Texto para mostrar a pontuação do adversário
    public TextMeshProUGUI victoryMessageText; // Texto para mostrar a mensagem de vitória

    // Lista de decks disponíveis (deve ser a mesma da cena de seleção)
    public List<Deck_ScriptableObject> decks;

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

    private void Start()
    {
        // Inicializa o texto da pontuação
        UpdateScoreUI();
        victoryMessageText.gameObject.SetActive(false); // Oculta a mensagem de vitória inicialmente

        // Recupera o índice do deck escolhido
        int selectedDeckIndex = PlayerPrefs.GetInt("SelectedDeckIndex", 0);

        // Verifica se o índice é válido
        if (selectedDeckIndex >= 0 && selectedDeckIndex < decks.Count)
        {
            Deck_ScriptableObject selectedDeck = decks[selectedDeckIndex];
            Debug.Log("Deck carregado: " + selectedDeck.deckName);

            // Passa o deck escolhido para o CardManager
            CardManager cardManager = FindObjectOfType<CardManager>();
            if (cardManager != null)
            {
                cardManager.SetDeck(selectedDeck);
                cardManager.currentDeck = selectedDeck;
            }
            else
            {
                Debug.LogError("CardManager não encontrado na cena.");
            }
        }
        else
        {
            Debug.LogError("Índice de deck inválido.");
        }
    }

    /// <summary>
    /// Incrementa a pontuação do jogador.
    /// </summary>
    public void AddPlayerScore()
    {
        playerScore++;
        Debug.Log("Jogador ganhou 1 ponto! Pontuação atual: " + playerScore);
        UpdateScoreUI();
        CheckVictory("Jogador", playerScore);
    }

    /// <summary>
    /// Incrementa a pontuação do adversário.
    /// </summary>
    public void AddOpponentScore()
    {
        opponentScore++;
        Debug.Log("Adversário ganhou 1 ponto! Pontuação atual: " + opponentScore);
        UpdateScoreUI();
        CheckVictory("Adversário", opponentScore);
    }

    /// <summary>
    /// Atualiza o texto da pontuação na interface do usuário.
    /// </summary>
    private void UpdateScoreUI()
    {
        playerScoreText.text = "Jogador: " + playerScore.ToString();
        opponentScoreText.text = "Adversário: " + opponentScore.ToString();
    }

    /// <summary>
    /// Verifica se alguém alcançou a pontuação máxima.
    /// </summary>
    private void CheckVictory(string winner, int score)
    {
        if (score >= maxScore)
        {
            Debug.Log(winner + " venceu o jogo!");
            EndGame(winner);
        }
    }

    /// <summary>
    /// Finaliza o jogo e exibe a mensagem de vitória.
    /// </summary>
    private void EndGame(string winner)
    {
        // Exibe a mensagem de vitória
        victoryMessageText.text = "Parabéns, " + winner + " venceu!";
        victoryMessageText.gameObject.SetActive(true);

        // Aqui você pode implementar lógica adicional, como travar o jogo ou retornar ao menu.
    }
}
