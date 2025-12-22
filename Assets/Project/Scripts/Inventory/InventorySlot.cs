[System.Serializable]
public class InventorySlot
{
    public ItemSO item;
    public int count;

    public InventorySlot(ItemSO item, int count)
    {
        this.item = item;
        this.count = count;
    }
}
