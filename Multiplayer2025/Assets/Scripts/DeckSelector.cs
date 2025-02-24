using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckSelector : MonoBehaviour
{
    // Lista de decks dispon�veis (atribu�dos no Inspector)
    public List<Deck_ScriptableObject> decks;

    // M�todo para selecionar um deck
    public void SelectDeck(int deckIndex)
    {
        if (deckIndex >= 0 && deckIndex < decks.Count)
        {
            // Salva o �ndice do deck escolhido
            PlayerPrefs.SetInt("SelectedDeckIndex", deckIndex);
            PlayerPrefs.Save(); // Garante que os dados sejam salvos

            Debug.Log("Deck selecionado: " + decks[deckIndex].deckName);

            // Carrega a cena do jogo
            //SceneManager.LoadScene(2);
        }
        else
        {
            Debug.LogError("�ndice de deck inv�lido.");
        }
    }
}
