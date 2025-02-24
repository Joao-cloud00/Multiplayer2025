using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Lista de prefabs de cartas que podem ser instanciadas
    //public List<GameObject> cardPrefabs;

    // Array de slots onde as cartas serão instanciadas
    public GameObject[] slots;

    public Deck_ScriptableObject currentDeck; //deck atual escolhido pelo jogador

    void Start()
    {

    }

    public void BuyRandomCard()
    {
        if (currentDeck == null || currentDeck.cards.Count == 0)
        {
            Debug.LogError("Nenhum deck foi definido ou o deck está vazio.");
            return;
        }

        // Verifica se há slots disponíveis
        foreach (GameObject slot in slots)
        {
            if (slot.GetComponent<Mao>().isTable != false) // Verifica se o slot está vazio
            {
                // Escolhe uma carta aleatória do deck atual
                GameObject randomCardPrefab = currentDeck.cards[Random.Range(0, currentDeck.cards.Count)];

                // Instancia a carta no slot
                GameObject cardInstance = Instantiate(randomCardPrefab, slot.transform.position, slot.transform.rotation);
                slot.GetComponent<Mao>().isTable = false;

                // Define o slot como pai da carta
                cardInstance.transform.SetParent(slot.transform);

                Debug.Log("Carta comprada do deck: " + currentDeck.deckName);

                // Sai do loop após instanciar a carta
                return;
            }
        }

        // Se não houver slots disponíveis, exibe uma mensagem
        Debug.Log("Não há slots disponíveis para novas cartas.");
    }

    // Método para definir o deck atual
    public void SetDeck(Deck_ScriptableObject deck)
    {
        foreach (GameObject slot in slots)
        {
            if (slot.transform.childCount == 0) // Verifica se o slot está vazio
            {
                // Escolhe uma carta aleatória do deck
                GameObject randomCardPrefab = deck.cards[Random.Range(0, deck.cards.Count)];

                // Instancia a carta no slot
                GameObject cardInstance = Instantiate(randomCardPrefab, slot.transform.position, slot.transform.rotation);

                // Define o slot como pai da carta
                cardInstance.transform.SetParent(slot.transform);
            }
        }
    }
}