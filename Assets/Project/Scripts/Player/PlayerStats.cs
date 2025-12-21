using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private int maxHealth = 80;
    [SerializeField] private int maxStamina = 80;

    public int CurrentHealth { get; private set; }
    public int CurrentStamina { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        CurrentStamina = maxStamina;
    }

    // --------------------
    // HEALTH
    // --------------------
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
    }

    // --------------------
    // STAMINA
    // --------------------
    public bool ConsumeStamina(int amount)
    {
        if (CurrentStamina < amount)
            return false;

        CurrentStamina -= amount;
        CurrentStamina = Mathf.Clamp(CurrentStamina, 0, maxStamina);
        return true;
    }

    public void RestoreStamina(int amount)
    {
        CurrentStamina += amount;
        CurrentStamina = Mathf.Clamp(CurrentStamina, 0, maxStamina);
    }

    // --------------------
    // DEATH
    // --------------------
    private void Die()
    {
        Debug.Log("Player died - Mountain Spirit intervenes");

        // Dağın Ruhu kurtarır
        CurrentHealth = 1;
        CurrentStamina = 1;

        // Buraya ileride:
        // - Eve ışınlanma
        // - Gün etkisi
        // eklenecek
    }

    public int PickaxeTier { get; private set; } = 1;

// Test için geçici
public void SetPickaxeTier(int tier)
{
    PickaxeTier = tier;
}

}
