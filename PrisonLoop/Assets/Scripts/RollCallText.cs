using System;
using TMPro;
using UnityEngine;

public class RollCallText :MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI text;
    private void Start()
    {

        int daysLeft = GameManager.Instance.MaxDay+1 - GameManager.Instance.CurrentDay;
        if (daysLeft > 1)
        {
            text.text = "Only " + daysLeft +" days left until execution.";
        }
        else
        {
            text.text = "Only 1 day left until execution.";
        }
    }
}
