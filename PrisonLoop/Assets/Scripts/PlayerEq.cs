using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEq : MonoBehaviour
{
    public static Action OnItemAdd;
    public static Action OnItemDrop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int capacity;
    private int currentCapacity;

    private List<ItemSO> items = new List<ItemSO>();

    public bool removeItem(ItemSO item)
    {
        if (items.Remove(item))
        {
            capacity -= item.Size;
            OnItemDrop?.Invoke();
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
}
