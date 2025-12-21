using TMPro;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private string itemId;

    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        if (InventoryManager.Instance == null) return;

        int count = InventoryManager.Instance.GetItemCount(itemId);
        countText.text = $"x{count}";
    }
}
