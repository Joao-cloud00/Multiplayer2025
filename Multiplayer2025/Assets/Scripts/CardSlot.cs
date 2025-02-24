using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public bool isOccupied = false; // Indica se o slot está ocupado
    private GameObject currentCard; // Referência para a carta atual no slot
    public bool isAttackSlot = false; // Define se este slot é o slot de ataque

    public void PlaceCard(GameObject card)
    {
        if (!isOccupied)
        {
            // Coloca a carta no slot
            currentCard = card;
            card.transform.position = transform.position;
            isOccupied = true;
            card.GetComponentInParent<Mao>().isTable = true;

            if (!isAttackSlot)
            {
                gameObject.SetActive(false); // Desativa o slot se for um slot de espera
            }
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

            if (!isAttackSlot)
            {
                gameObject.SetActive(true); // Reativa o slot de espera ao remover a carta
            }
        }
    }
}
