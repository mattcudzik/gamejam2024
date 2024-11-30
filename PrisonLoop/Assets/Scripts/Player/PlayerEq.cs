using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEq : MonoBehaviour
{
    public static Action OnItemAdd;
    public static Action OnItemDrop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int capacity;
    private int currentCapacity;

    public TextMeshProUGUI resultText;

    private List<ItemSO> items = new List<ItemSO>();

    public bool removeItem(ItemSO item)
    {
        if (items.Remove(item))
        {
            capacity -= item.Size;
            OnItemDrop?.Invoke();
            updateEqView();
            return true;
        }

        return false;
    }

    public bool addItem(ItemSO item)
    {
        if (CanGetItem(item))
        {
            items.Add(item);
            currentCapacity += item.Size;
            OnItemAdd?.Invoke();
            updateEqView();
            return true;
        }
        return false;
    }

    int getFreeCapacity()
    { 
        return capacity- currentCapacity;
    }
    public bool CanGetItem(ItemSO item)
    {
        if (item.Size <= getFreeCapacity())
            return true;
        return false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateEqView()
    {
        resultText.text = GetEqStr();
    }

    public string GetEqStr()
    {
        string result = "Ekwipunek "+currentCapacity+"/"+capacity+"\n";
        int idx = 1;
        foreach(ItemSO item in items)
        { 
            result += "["+idx++ +"] " + item.toStr() + "\n";
        }
        
        return result;
    }
}
