using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private int amount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        InventoryManager.Instance.AddItem(item, amount);
        Destroy(gameObject);
    }
}
