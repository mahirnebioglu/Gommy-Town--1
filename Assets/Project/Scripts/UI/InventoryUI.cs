using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private void OnEnable()
    {
        InventoryManager.Instance.OnInventoryChanged += Refresh;
    }

    private void OnDisable()
    {
        InventoryManager.Instance.OnInventoryChanged -= Refresh;
    }

    private void Refresh()
    {
        // Slotlar kendi kendini güncelliyor
    }
}
