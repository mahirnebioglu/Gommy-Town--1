using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventorySlot> slots = new List<InventorySlot>();
    public Action OnInventoryChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(ItemSO item, int amount)
    {
        InventorySlot slot = slots.Find(s => s.item == item);

        if (slot != null)
            slot.count += amount;
        else
            slots.Add(new InventorySlot(item, amount));

        OnInventoryChanged?.Invoke();
    }
}
