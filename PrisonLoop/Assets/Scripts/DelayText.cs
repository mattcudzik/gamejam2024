using TMPro;
using UnityEngine;

public class DelayText:MonoBehaviour
{
    [SerializeField] TextMeshProUGUI reasonText;

    public void SetText(string text)
    {
        reasonText.text = text;
    }
}
