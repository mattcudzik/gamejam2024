using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TradeUI : MonoBehaviour
{
    public GameObject UI;
    public Image desiredItem;
    public Image offeredItem;
    public void ShowUI()
    {
        UI.SetActive(true);
    }

    public void HideUI()
    {
        UI.SetActive(false);
    }
    public void SetUp(TradeOffer tradeOffer)
    {
        desiredItem.sprite = tradeOffer.desiredItem.Sprite;
        offeredItem.sprite = tradeOffer.offeredItem.Sprite;
    }
}
