using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHP = 60;
    private int currentHP;

    [Header("Drop")]
    [SerializeField] private GameObject dropPrefab;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void Mine(int damage)
    {
        currentHP -= damage;

        // 🔊 HIT EVENT
        GameEvents.RaiseMiningHit();

        if (currentHP <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        // 🔊 BREAK EVENT
        GameEvents.RaiseMiningBreak();

        if (dropPrefab != null)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
