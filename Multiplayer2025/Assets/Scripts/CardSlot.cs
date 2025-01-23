using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public bool isOccupied = false; // Indica se o slot está ocupado
    private GameObject currentCard; // Referência para a carta atual no slot

    public void PlaceCard(GameObject card)
    {
        if (!isOccupied)
        {
            // Coloca a carta no slot
            currentCard = card;
            card.transform.position = transform.position;
            isOccupied = true;
        }
        else
        {
            Debug.Log("Slot já está ocupado!");
        }
    }

    public void RemoveCard(GameObject card)
    {
        // Verifica se a carta a ser removida é a atual no slot
        if (currentCard == card)
        {
            currentCard = null;
            isOccupied = false;
        }
    }
}


