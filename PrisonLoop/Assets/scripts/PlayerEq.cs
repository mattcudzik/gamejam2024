using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEq : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int capacity;
    private int currentCapacity;

    private List<ItemSO> items;

    int getFreeCapacity()
    { 
        return capacity- currentCapacity;
    }
    bool CanGetItem(ItemSO item)
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
