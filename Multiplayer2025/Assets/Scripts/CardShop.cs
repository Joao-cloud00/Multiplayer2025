using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShop : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject cardPrefab;

    public void BuyCard()
    {
            foreach (GameObject slot in slots)
            {
                if (slot.transform.childCount == 0)
                {
                    GameObject cardInstance = Instantiate(cardPrefab, slot.transform.position, slot.transform.rotation);
                    cardInstance.transform.SetParent(slot.transform);

                    return;
                }
            }
    }

}
