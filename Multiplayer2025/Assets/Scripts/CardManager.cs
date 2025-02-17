using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Lista de prefabs de cartas que podem ser instanciadas
    public List<GameObject> cardPrefabs;

    // Array de slots onde as cartas serão instanciadas
    public GameObject[] slots;

    void Start()
    {
        // Verifica se há prefabs de cartas na lista
        if (cardPrefabs == null || cardPrefabs.Count == 0)
        {
            Debug.LogError("Nenhum prefab de carta foi atribuído na lista.");
            return;
        }

        // Itera sobre cada slot e instancia uma carta aleatória
        foreach (GameObject slot in slots)
        {
            if (slot != null)
            {
                // Escolhe um prefab aleatório da lista
                GameObject randomCardPrefab = cardPrefabs[Random.Range(0, cardPrefabs.Count)];

                // Instancia a carta no slot
                GameObject cardInstance = Instantiate(randomCardPrefab, slot.transform.position, slot.transform.rotation);

                //// Define o slot como pai da carta (opcional)
                //cardInstance.transform.SetParent(slot.transform);
            }
        }
    }
    public void BuyRandomCard()
    {
        // Verifica se há slots disponíveis
        foreach (GameObject slot in slots)
        {
            if (slot.transform.childCount == 0) // Verifica se o slot está vazio
            {
                // Escolhe um prefab aleatório da lista
                GameObject randomCardPrefab = cardPrefabs[Random.Range(0, cardPrefabs.Count)];

                // Instancia a carta no slot
                GameObject cardInstance = Instantiate(randomCardPrefab, slot.transform.position, slot.transform.rotation);

                // Define o slot como pai da carta
                cardInstance.transform.SetParent(slot.transform);

                // Sai do loop após instanciar a carta
                return;
            }
        }

        // Se não houver slots disponíveis, exibe uma mensagem
        Debug.Log("Não há slots disponíveis para novas cartas.");
    }
}