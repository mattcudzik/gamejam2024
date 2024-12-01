using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    public Image desiredItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
    public void SetUp(HelpOffer tradeOffer)
    {
        desiredItem.sprite = tradeOffer.desiredItem.Sprite;
    }
}
