using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Mining")]
    [SerializeField] private Vector2 miningOffset = new Vector2(0f, 0.8f);
    [SerializeField] private float miningRadius = 1.2f;
    [SerializeField] private int miningDamage = 15; // ✅ DAMAGE BURADA

    private Rigidbody2D rb;
    private Vector2 movement;
    private PlayerStats playerStats;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        // --------------------
        // MOVEMENT INPUT
        // --------------------
        movement = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) movement.y += 1;
        if (Keyboard.current.sKey.isPressed) movement.y -= 1;
        if (Keyboard.current.aKey.isPressed) movement.x -= 1;
        if (Keyboard.current.dKey.isPressed) movement.x += 1;

        movement.Normalize();

        // --------------------
        // MINING (E KEY)
        // --------------------
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryMine();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // ==========================
    // MINING LOGIC
    // ==========================
    private void TryMine()
    {
        if (!playerStats.ConsumeStamina(2))
        {
            Debug.Log("Not enough stamina to mine!");
            return;
        }

        animator.SetTrigger("Mine");

        Vector2 miningPoint = (Vector2)transform.position + miningOffset;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            miningPoint,
            miningRadius,
            LayerMask.GetMask("Mineable")
        );

        foreach (Collider2D hit in hits)
        {
            Stalagmite stalagmite = hit.GetComponent<Stalagmite>();
            if (stalagmite != null)
            {
                // ✅ DOĞRU ÇAĞRI
                stalagmite.Mine(miningDamage);
                break;
            }
        }
    }

    // ==========================
    // DEBUG GIZMOS
    // ==========================
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 miningPoint = (Vector2)transform.position + miningOffset;
        Gizmos.DrawWireSphere(miningPoint, miningRadius);
    }
}
