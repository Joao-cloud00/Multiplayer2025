using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDeck", menuName = "Card Game/Deck")]
public class Deck_ScriptableObject : ScriptableObject
{
    public string deckName;
    public List<GameObject> cards;
}
