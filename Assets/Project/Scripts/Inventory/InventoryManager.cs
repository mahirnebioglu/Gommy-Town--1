using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private Dictionary<ItemSO, int> items = new Dictionary<ItemSO, int>();

    public event Action OnInventoryChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // ✅ ADD ITEM
    public void AddItem(ItemSO item, int amount)
    {
        if (!items.ContainsKey(item))
            items[item] = 0;

        items[item] += amount;

        OnInventoryChanged?.Invoke();
    }

    // ✅ GET COUNT
    public int GetItemCount(ItemSO item)
    {
        return items.TryGetValue(item, out int count) ? count : 0;
    }

    // ✅ RESET (A.5.4 için)
    public void ResetInventory()
    {
        items.Clear();
        OnInventoryChanged?.Invoke();
    }
}
