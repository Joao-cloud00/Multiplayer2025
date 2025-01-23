using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [Header("Configurações do Deck")]
    public List<GameObject> deck = new List<GameObject>(); 
    public Transform handArea;                             
    public float cardSpacing = 50f;                      

    private List<GameObject> hand = new List<GameObject>(); 

    private void Start()
    {
        ShuffleDeck();      
        DrawInitialHand(3); 
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            GameObject temp = deck[i];
            int randomIndex = Random.Range(0, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    private void DrawInitialHand(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            
            GameObject cardPrefab = deck[0];
            deck.RemoveAt(0);

            AddCardToHand(cardPrefab);
            Debug.Log("carta comprada");
        }
        else
        {
            Debug.Log("O deck está vazio!");
        }
    }

    private void AddCardToHand(GameObject cardPrefab)
    {
        // Instancia a carta como filha da área da mão
        GameObject newCard = Instantiate(cardPrefab, handArea);

        // Garante que o RectTransform seja ajustado
        RectTransform rectTransform = newCard.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.localScale = Vector3.one; // Define a escala como 1 (tamanho original)
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // Centraliza as âncoras
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f); // Centraliza o pivô
            rectTransform.sizeDelta = new Vector2(150, 200); // Define o tamanho (ajuste aqui para o desejado)
            rectTransform.anchoredPosition3D = Vector3.zero; // Zera a posição local
        }

        // Adiciona a carta à lista da mão
        hand.Add(newCard);

        // Atualiza o posicionamento das cartas na mão
        UpdateHandPosition();
    }


    private void UpdateHandPosition()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            Vector3 position = new Vector3(i * cardSpacing, 0, 0);
            hand[i].GetComponent<RectTransform>().anchoredPosition = position;
        }
    }
}