using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    // Lista de decks disponíveis (deve ser a mesma da cena de seleção)
    public List<Deck_ScriptableObject> decks;

    void Start()
    {
        // Recupera o índice do deck escolhido
        int selectedDeckIndex = PlayerPrefs.GetInt("SelectedDeckIndex", 0);

        // Verifica se o índice é válido
        if (selectedDeckIndex >= 0 && selectedDeckIndex < decks.Count)
        {
            Deck_ScriptableObject selectedDeck = decks[selectedDeckIndex];
            Debug.Log("Deck carregado: " + selectedDeck.deckName);

            // Instancia as cartas do deck nos slots (usando o script CardManager)
            CardManager cardManager = FindObjectOfType<CardManager>();
            if (cardManager != null)
            {
                cardManager.SetDeck(selectedDeck);
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
}
