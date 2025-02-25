using UnityEngine;
using UnityEngine.UI;

public class SelecaoBaralho : MonoBehaviour
{
    public GridLayoutGroup gridLayout1; // Primeiro GridLayout
    public GridLayoutGroup gridLayout2; // Segundo GridLayout

    void Start()
    {
        // Carrega os baralhos dos PlayerPrefs e exibe as cartas
        CarregarBaralho(0, gridLayout1);  // Carrega o primeiro baralho
        CarregarBaralho(1, gridLayout2);  // Carrega o segundo baralho
    }

    public void CarregarBaralho(int baralhoIndex, GridLayoutGroup gridLayout)
    {
        int count = PlayerPrefs.GetInt($"Baralho_{baralhoIndex}_Count");  // Recupera a quantidade de cartas
        for (int i = 0; i < count; i++)
        {
            string nomeCarta = PlayerPrefs.GetString($"Baralho_{baralhoIndex}_Carta_{i}");  // Recupera o nome da carta
            Sprite cartaSprite = Resources.Load<Sprite>(nomeCarta);  // Carrega a carta com o nome

            if (cartaSprite != null)
            {
                GameObject cartaObj = new GameObject(nomeCarta);
                Image cartaImage = cartaObj.AddComponent<Image>();
                cartaImage.sprite = cartaSprite;
                cartaObj.transform.SetParent(gridLayout.transform, false);  // Adiciona ao GridLayout
            }
        }
    }
}
