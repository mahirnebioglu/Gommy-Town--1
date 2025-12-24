using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        // Oyun etkinleştiğinde de hareketi sıfırla
        ResetMovement();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        // Oyun başında hareketi sıfırla
        ResetMovement();
    }

    private void ResetMovement()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private void FixedUpdate()
{
    // Dead zone – mikro inputları yok et 
    if (moveInput.sqrMagnitude < 0.01f)
    {
        rb.linearVelocity = Vector2.zero;
        return;
    }

    // Normalize ederek sabit hız sağla
    Vector2 move = moveInput.normalized * moveSpeed;
    rb.linearVelocity = move;
}
}
