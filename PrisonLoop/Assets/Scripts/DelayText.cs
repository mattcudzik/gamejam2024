using System;
using TMPro;
using UnityEngine;

public class DelayText:MonoBehaviour
{
    [SerializeField] TextMeshProUGUI reasonText;

    private void Awake()
    {
        SetText();
    }

    public void SetText()
    {
        reasonText.text = GameManager.Instance.Timer.myReason;
    }
}
