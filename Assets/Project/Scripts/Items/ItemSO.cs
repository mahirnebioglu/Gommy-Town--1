using UnityEngine;

[CreateAssetMenu(
    fileName = "NewItem",
    menuName = "GommyTown/Item"
)]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool stackable = true;
}
