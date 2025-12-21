using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public static event Action OnInventoryChanged;

    private Dictionary<string, int> items = new Dictionary<string, int>();

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

    public void AddItem(string itemId, int amount)
    {
        if (items.ContainsKey(itemId))
            items[itemId] += amount;
        else
            items[itemId] = amount;

        Debug.Log($"Inventory → {itemId}: {items[itemId]}");

        OnInventoryChanged?.Invoke(); // 🔥 KRİTİK
    }

    public int GetItemCount(string itemId)
    {
        return items.TryGetValue(itemId, out int count) ? count : 0;
    }
}
