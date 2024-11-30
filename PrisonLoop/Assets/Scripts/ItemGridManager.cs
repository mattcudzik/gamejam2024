using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemGridManager : MonoBehaviour
{
    public List<ItemSO> items; // Lista elementów ItemSO
    public GameObject tilePrefab;                  // Prefab kafelka
    public Transform contentTransform;             // Referencja do Content w Grid Layout
    private Vector2 baseSize; // Domyœlny rozmiar kafelka (wyci¹gniêty z prefabrykatu)

    void Start()
    {
        items = GameManager.Instance.PlayerEq.GetItems();
        PlayerEq.OnItemAdd += PopulateGrid;
        PlayerEq.OnItemDrop += PopulateGrid;

        RectTransform prefabRectTransform = tilePrefab.GetComponent<RectTransform>();
        baseSize = prefabRectTransform.sizeDelta;

        PopulateGrid();
    }

    void PopulateGrid()
    {
        // Usuñ istniej¹ce kafelki
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        // Dodaj kafelek dla ka¿dego elementu
        foreach (var item in items)
        {
            GameObject newTile = Instantiate(tilePrefab, contentTransform);

            // Ustaw Sprite elementu
            Image spriteImage = newTile.transform.Find("Image").GetComponent<Image>();
            spriteImage.sprite = item.Sprite;

            RectTransform rectTransform = newTile.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(baseSize.x, baseSize.y * Mathf.Max(1, item.Size));

            // Opcjonalnie: Ustaw nazwê elementu jako tekst
            //Text textComponent = newTile.transform.Find("Text").GetComponent<Text>();
            //textComponent.text = item.Type.ToString(); // Nazwa typu elementu
        }
    }
}
