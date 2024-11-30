using UnityEngine;
using UnityEngine.Serialization;

public class TradeUI : MonoBehaviour
{
    public GameObject UI;
    public void ShowUI()
    {
        UI.SetActive(true);
    }

    public void HideUI()
    {
        UI.SetActive(false);
    }
}
