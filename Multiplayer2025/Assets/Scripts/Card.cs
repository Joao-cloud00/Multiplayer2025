using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private bool isSelected = false;
    private CardSlot currentSlot;
    private bool isInWaitingSlot = false; // Nova vari�vel para verificar se est� no slot de espera

    // Atributos de vida, dano e estado
    public int health = 10;
    public int damage = 5;
    public bool isPlayerCard; // Identifica se � do jogador ou do advers�rio
    public bool isOnTable = false; // Verifica se a carta est� na mesa
    public Text txtLife;

    void Update()
    {
        string life = health.ToString();
        txtLife.text = life;

        if (isSelected && Input.GetMouseButtonDown(0)) // Detecta clique no espa�o
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                CardSlot slot = hit.collider.GetComponent<CardSlot>();
                if (slot != null)
                {
                    if (!isInWaitingSlot) // Se ainda n�o est� no slot de espera
                    {
                        if(slot != slot.isAttackSlot)
                        {
                            MoveToWaitingSlot(slot);
                        }
                    }
                    else if (!isOnTable) // Se j� est� no slot de espera, pode ir para o slot de ataque
                    {
                        MoveToAttackSlot(slot);
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        // Quando a carta est� na mesa, ela pode ser selecionada para atacar
        if (isOnTable)
        {
            if (AttackSystem.Instance != null && AttackSystem.Instance.attackingCard != null)
            {
                // Realiza o ataque se a carta clicada for alvo
                AttackSystem.Instance.PerformAttack(this);
            }
            else
            {
                // Seleciona a carta para atacar
                isSelected = true;
                Debug.Log("Carta selecionada!");
                AttackSystem.Instance.SetAttackingCard(this);
            }
        }
        else
        {
            // Quando n�o est� na mesa, apenas permite a sele��o para coloca��o
            isSelected = true;
            Debug.Log("Selecione um slot para colocar a carta na mesa.");
        }
    }

    private void MoveToWaitingSlot(CardSlot slot)
    {
        if (currentSlot != null)
        {
            currentSlot.RemoveCard(gameObject);
        }
        slot.PlaceCard(gameObject);
        currentSlot = slot;
        isInWaitingSlot = true;
        isSelected = false;
    }

    private void MoveToAttackSlot(CardSlot slot)
    {
            if (currentSlot != null)
            {
                currentSlot.RemoveCard(gameObject);
            }
            slot.PlaceCard(gameObject);
            currentSlot = slot;
            isOnTable = true;
            isSelected = false;
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

        if (!isPlayerCard)
        {
            GameManager.Instance.AddPlayerScore(); // Pontua��o para o jogador
            Destroy(gameObject);
        }
        else
        {
            GameManager.Instance.AddOpponentScore(); // Pontua��o para o advers�rio
            Destroy(gameObject);
        }
    }
}
