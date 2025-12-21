using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private int maxHP = 60;
    [SerializeField] private GameObject dropPrefab;
    private int currentHP;

    [Header("Sprites")]
    [SerializeField] private Sprite fullSprite;   // 60
    [SerializeField] private Sprite sprite45;     // 45
    [SerializeField] private Sprite sprite30;     // 30
    [SerializeField] private Sprite sprite15;     // 15

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHP = maxHP;
        UpdateSprite();
    }

    public void Mine(int pickaxeTier)
    {
        CameraShake.Instance?.Shake(0.08f, 0.03f);
        
        int damage = GetDamageByTier(pickaxeTier);
        currentHP -= damage;

        Debug.Log($"Stalagmite hit! Tier:{pickaxeTier} Damage:{damage} HP:{currentHP}");

        UpdateSprite();

        if (currentHP <= 0)
        {
            Break();
        }
    }

    private int GetDamageByTier(int tier)
    {
        switch (tier)
        {
            case 1: return 15; // 4 hit
            case 2: return 30; // 2 hit
            case 3: return 60; // 1–2 hit
            case 4: return 999; // instant
            default: return 15;
        }
    }

    private void UpdateSprite()
    {
        if (currentHP > 45)
            sr.sprite = fullSprite;
        else if (currentHP > 30)
            sr.sprite = sprite45;
        else if (currentHP > 15)
            sr.sprite = sprite30;
        else if (currentHP > 0)
            sr.sprite = sprite15;
    }

    private void Break()
{
    Instantiate(dropPrefab, transform.position, Quaternion.identity);
    Destroy(gameObject);
}
}
