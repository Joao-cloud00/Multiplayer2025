using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private bool isSelected = false;
    private CardSlot currentSlot;

    // Atributos de vida e dano
    public int health = 10;
    public int damage = 5;

    void Update()
    {
        if (isSelected && Input.GetMouseButtonDown(0)) // Detecta clique no espa�o
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                CardSlot slot = hit.collider.GetComponent<CardSlot>();
                if (slot != null)
                {
                    // Se j� est� em um slot, remove a carta do slot anterior
                    if (currentSlot != null)
                    {
                        currentSlot.RemoveCard(gameObject);
                    }

                    // Coloca a carta no novo slot
                    slot.PlaceCard(gameObject);
                    currentSlot = slot; // Atualiza o slot atual
                    isSelected = false; // Cancela a sele��o
                }
            }
        }
    }

    private void OnMouseDown()
    {
        // Seleciona a carta ao clicar
        isSelected = true;
        Debug.Log("Carta selecionada!");
    }

    // M�todo para receber dano
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(gameObject.name + " recebeu " + amount + " de dano. Vida restante: " + health);

        if (health <= 0)
        {
            DestroyCard();
        }
    }

    // M�todo para destruir a carta
    private void DestroyCard()
    {
        Debug.Log(gameObject.name + " foi destru�da!");

        // Libera o slot, se estiver em um
        if (currentSlot != null)
        {
            currentSlot.RemoveCard(gameObject);
            currentSlot = null;
        }

        // Destroi o objeto da carta
        Destroy(gameObject);
    }
}
