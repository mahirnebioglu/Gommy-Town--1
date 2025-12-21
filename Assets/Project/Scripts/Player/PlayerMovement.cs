using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Mining")]
    [SerializeField] private Vector2 miningOffset = new Vector2(0f, 0.8f);
    [SerializeField] private float miningRadius = 1.2f;

    private Animator animator;

    private Rigidbody2D rb;
    private Vector2 movement;
    private PlayerStats playerStats;

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
        // TEST: SPACE → STAMINA
        // --------------------
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!playerStats.ConsumeStamina(10))
            {
                Debug.Log("Not enough stamina!");
            }
        }

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
    animator.SetTrigger("Mine");

    if (!playerStats.ConsumeStamina(2))
    {
        Debug.Log("Not enough stamina to mine!");
        return;
    }

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
            int pickaxeTier = 1; // şimdilik sabit
            stalagmite.Mine(pickaxeTier);
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
