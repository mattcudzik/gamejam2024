using System;
using TMPro;
using UnityEngine;

public class DelayText:MonoBehaviour
{
    [SerializeField] TextMeshProUGUI reasonText;

    private void Start()
    {
        SetText();
    }

    public void SetText()
    {
        if (GameManager.Instance.Timer.myReason != null)
            reasonText.text = GameManager.Instance.Timer.myReason;
        else
        {
            reasonText.text="because you have not completed the task";
        }
    }
}
