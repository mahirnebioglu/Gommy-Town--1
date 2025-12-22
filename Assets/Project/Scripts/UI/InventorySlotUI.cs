using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI countText;

    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= Refresh;
    }

    private void Refresh()
    {
        if (item == null) return;

        int count = InventoryManager.Instance.GetItemCount(item);

        icon.sprite = item.icon;
        countText.text = count > 0 ? count.ToString() : "0";
    }
}
