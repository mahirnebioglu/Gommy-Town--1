using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public static event Action OnInventoryChanged;

    private Dictionary<ItemSO, int> items = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(ItemSO item, int amount)
    {
        if (item == null) return;

        if (items.ContainsKey(item))
            items[item] += amount;
        else
            items[item] = amount;

        Debug.Log($"Inventory → {item.itemName}: {items[item]}");

        OnInventoryChanged?.Invoke();
    }

    public int GetItemCount(ItemSO item)
    {
        return items.TryGetValue(item, out int count) ? count : 0;
    }
}
