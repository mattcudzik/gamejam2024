using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemGridManager : MonoBehaviour
{
    public List<ItemSO> items; // Lista element�w ItemSO
    public GameObject tilePrefab;                  // Prefab kafelka
    public Transform contentTransform;             // Referencja do Content w Grid Layout
    private Vector2 baseSize; // Domy�lny rozmiar kafelka (wyci�gni�ty z prefabrykatu)
    [SerializeField] private KeyCode interactionKey = KeyCode.Q;
    private PlayerEq playerEq;
    private int currentIdx = 0;
    void Start()
    {
        playerEq = GameManager.Instance.PlayerEq;
        items = playerEq.GetItems();
        
        if (items.Count == 0)
        {
            currentIdx = -1;
        }

        PlayerEq.OnItemAdd += PopulateGrid;
        PlayerEq.OnItemDrop += PopulateGrid;

        RectTransform prefabRectTransform = tilePrefab.GetComponent<RectTransform>();
        baseSize = prefabRectTransform.sizeDelta;

        PopulateGrid();
    }

    private void Update()
    {
        var shouldUpdate = false;
        if (Input.GetKeyDown(interactionKey) && currentIdx!=-1)
        {
            shouldUpdate |= playerEq.removeItem(items[currentIdx]);
            if (items.Count == 0)
            {
                currentIdx = -1;
            }
            if (currentIdx == items.Count - 1)
            {
                currentIdx--;
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        shouldUpdate |= scroll != 0;

        if (scroll > 0f)
        {
            if(currentIdx>0)
                currentIdx--;
        }
        else if (scroll < 0f)
        {
            if (currentIdx < items.Count -1)
                currentIdx++;
        }

        if (shouldUpdate)
        {
            PopulateGrid();
        }
    }

    void PopulateGrid()
    {
        // Usu� istniej�ce kafelki
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        // Dodaj kafelek dla ka�dego elementu
        if (currentIdx == -1 && items.Count > 0)
            currentIdx = 0;
        int idx = 0;
        foreach (var item in items)
        {
            GameObject newTile = Instantiate(tilePrefab, contentTransform);

            Image spriteImage = newTile.transform.Find("Image").GetComponent<Image>();
            if (currentIdx == idx)
            {
                Image spriteImageBg = newTile.transform.Find("Bg").GetComponent<Image>();
                Color currentColor = spriteImageBg.color;
                currentColor.a = 150.0f;
                spriteImageBg.color = currentColor;
            }
            spriteImage.sprite = item.Sprite;

            RectTransform rectTransform = newTile.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(baseSize.x, baseSize.y * Mathf.Max(1, item.Size));
            if (currentIdx == idx)
            {
                Image spriteImageBg = newTile.transform.Find("Bg").GetComponent<Image>();
                Color currentColor = spriteImageBg.color;
                currentColor.a = 150;
                spriteImageBg.color = currentColor;
                
            }
            idx++;
        }
    }
}
