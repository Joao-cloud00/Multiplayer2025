using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private bool isSelected = false;
    private CardSlot currentSlot;
    private bool isInWaitingSlot = false; // Nova variável para verificar se está no slot de espera

    // Atributos de vida, dano e estado
    public int health = 10;
    public int damage = 5;
    public bool isPlayerCard; // Identifica se é do jogador ou do adversário
    public bool isOnTable = false; // Verifica se a carta está na mesa
    public Text txtLife;

    void Update()
    {
        string life = health.ToString();
        txtLife.text = life;

        if (isSelected && Input.GetMouseButtonDown(0)) // Detecta clique no espaço
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                CardSlot slot = hit.collider.GetComponent<CardSlot>();
                if (slot != null)
                {
                    if (!isInWaitingSlot) // Se ainda não está no slot de espera
                    {
                        if(slot != slot.isAttackSlot)
                        {
                            MoveToWaitingSlot(slot);
                        }
                    }
                    else if (!isOnTable) // Se já está no slot de espera, pode ir para o slot de ataque
                    {
                        MoveToAttackSlot(slot);
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        // Quando a carta está na mesa, ela pode ser selecionada para atacar
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
            // Quando não está na mesa, apenas permite a seleção para colocação
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

    // Método para receber dano
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(gameObject.name + " recebeu " + amount + " de dano. Vida restante: " + health);

        if (health <= 0)
        {
            DestroyCard();
        }
    }

    // Método para destruir a carta
    private void DestroyCard()
    {
        Debug.Log(gameObject.name + " foi destruída!");

        // Libera o slot, se estiver em um
        if (currentSlot != null)
        {
            currentSlot.RemoveCard(gameObject);
            currentSlot = null;
        }

        if (!isPlayerCard)
        {
            GameManager.Instance.AddPlayerScore(); // Pontuação para o jogador
            Destroy(gameObject);
        }
        else
        {
            GameManager.Instance.AddOpponentScore(); // Pontuação para o adversário
            Destroy(gameObject);
        }
    }
}
