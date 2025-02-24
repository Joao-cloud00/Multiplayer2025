using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    // Lista de decks dispon�veis (deve ser a mesma da cena de sele��o)
    public List<Deck_ScriptableObject> decks;

    void Start()
    {
        // Recupera o �ndice do deck escolhido
        int selectedDeckIndex = PlayerPrefs.GetInt("SelectedDeckIndex", 0);

        // Verifica se o �ndice � v�lido
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
                Debug.LogError("CardManager n�o encontrado na cena.");
            }
        }
        else
        {
            Debug.LogError("�ndice de deck inv�lido.");
        }
    }
}
