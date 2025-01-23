using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public static AttackSystem Instance; // Singleton para fácil acesso
    public Card attackingCard; // Carta que está atacando

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAttackingCard(Card card)
    {
        if (card.isOnTable) // Apenas cartas na mesa podem atacar
        {
            attackingCard = card;
            Debug.Log("Carta " + card.name + " está pronta para atacar!");
        }
        else
        {
            Debug.Log("Você não pode atacar com uma carta que não está na mesa.");
        }
    }

    public void PerformAttack(Card targetCard)
    {
        if (attackingCard != null && targetCard != null)
        {
            // Verifica se ambas as cartas estão na mesa e são de jogadores diferentes
            if (attackingCard.isOnTable && targetCard.isOnTable && attackingCard.isPlayerCard != targetCard.isPlayerCard)
            {
                Debug.Log(attackingCard.name + " atacou " + targetCard.name + " causando " + attackingCard.damage + " de dano.");
                targetCard.TakeDamage(attackingCard.damage);
                attackingCard = null; // Reseta a carta atacante após o ataque
            }
            else
            {
                Debug.Log("Você não pode atacar esta carta.");
            }
        }
    }
}

