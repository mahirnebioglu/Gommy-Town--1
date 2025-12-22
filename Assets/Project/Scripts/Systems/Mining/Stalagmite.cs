using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHP = 60;
    private int currentHP;

    [Header("Drop")]
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private int dropAmount = 1;

    private void Awake()
    {
        currentHP = maxHP;
    }

    // 🔹 PlayerMovement burayı çağırıyor
    public void Mine(int damage)
    {
        TakeDamage(damage);
    }

    // ==========================
    // DAMAGE & SHAKE
    // ==========================
    private void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"Stalagmite hit! HP: {currentHP}");

        // ✅ CAMERA SHAKE BURADA
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake();
        }

        if (currentHP <= 0)
        {
            Break();
        }
    }

    // ==========================
    // BREAK & DROP
    // ==========================
    private void Break()
    {
        Debug.Log("Stalagmite broken");

        if (dropPrefab != null)
        {
            for (int i = 0; i < dropAmount; i++)
            {
                Instantiate(
                    dropPrefab,
                    transform.position + Random.insideUnitSphere * 0.2f,
                    Quaternion.identity
                );
            }
        }

        Destroy(gameObject);
    }
}
