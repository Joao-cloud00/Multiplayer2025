using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform handArea;
    public GameObject cardPrefab;


    void Start()
    {
        ShuffleDeck();
        DrawInitialCards(3);
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++) 
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(0, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

    }

    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            Card drawnCard = deck[0];
            deck.RemoveAt(0);
            AddCardToHand(drawnCard);
        }
        else
        {
            Debug.Log("O deck está vazio");
        }
    }

    private void AddCardToHand(Card card)
    {
        GameObject newCard = Instantiate(cardPrefab, handArea);
        
    }

    private void DrawInitialCards(int count)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            DrawCard();
        }
    }

}
