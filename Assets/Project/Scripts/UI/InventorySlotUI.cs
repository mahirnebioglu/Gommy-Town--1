using TMPro;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private TMP_Text countText;

    private void OnEnable()
    {
        InventoryManager.Instance.OnInventoryChanged += UpdateSlot;
        UpdateSlot();
    }

    private void OnDisable()
    {
        InventoryManager.Instance.OnInventoryChanged -= UpdateSlot;
    }

    private void UpdateSlot()
    {
        int count = InventoryManager.Instance.GetItemCount(item);
        countText.text = count > 0 ? count.ToString() : "0";
    }
}
