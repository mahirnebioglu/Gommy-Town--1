using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private string itemId = "Stone";
    [SerializeField] private int amount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        InventoryManager.Instance.AddItem(itemId, amount);
        Debug.Log(itemId + " picked up x" + amount);

        Destroy(gameObject);
    }
}
