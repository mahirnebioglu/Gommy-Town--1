using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += Refresh;
    }

    private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= Refresh;
    }

    private void Refresh()
    {
        // Slotlar kendi kendini güncelliyor
    }
}
