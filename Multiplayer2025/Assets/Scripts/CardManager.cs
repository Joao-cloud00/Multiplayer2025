using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Lista de prefabs de cartas que podem ser instanciadas
    //public List<GameObject> cardPrefabs;

    // Array de slots onde as cartas ser�o instanciadas
    public GameObject[] slots;

    public Deck_ScriptableObject currentDeck; //deck atual escolhido pelo jogador

    void Start()
    {

    }

    public void BuyRandomCard()
    {
        if (currentDeck == null || currentDeck.cards.Count == 0)
        {
            Debug.LogError("Nenhum deck foi definido ou o deck est� vazio.");
            return;
        }

        // Verifica se h� slots dispon�veis
        foreach (GameObject slot in slots)
        {
            if (slot.GetComponent<Mao>().isTable != false) // Verifica se o slot est� vazio
            {
                // Escolhe uma carta aleat�ria do deck atual
                GameObject randomCardPrefab = currentDeck.cards[Random.Range(0, currentDeck.cards.Count)];

                // Instancia a carta no slot
                GameObject cardInstance = Instantiate(randomCardPrefab, slot.transform.position, slot.transform.rotation);
                slot.GetComponent<Mao>().isTable = false;

                // Define o slot como pai da carta
                cardInstance.transform.SetParent(slot.transform);

                Debug.Log("Carta comprada do deck: " + currentDeck.deckName);

                // Sai do loop ap�s instanciar a carta
                return;
            }
        }

        // Se n�o houver slots dispon�veis, exibe uma mensagem
        Debug.Log("N�o h� slots dispon�veis para novas cartas.");
    }

    // M�todo para definir o deck atual
    public void SetDeck(Deck_ScriptableObject deck)
    {
        foreach (GameObject slot in slots)
        {
            if (slot.transform.childCount == 0) // Verifica se o slot est� vazio
            {
                // Escolhe uma carta aleat�ria do deck
                GameObject randomCardPrefab = deck.cards[Random.Range(0, deck.cards.Count)];

                // Instancia a carta no slot
                GameObject cardInstance = Instantiate(randomCardPrefab, slot.transform.position, slot.transform.rotation);

                // Define o slot como pai da carta
                cardInstance.transform.SetParent(slot.transform);
            }
        }
    }
}