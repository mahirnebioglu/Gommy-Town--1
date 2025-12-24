using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMining : MonoBehaviour
{
    [Header("Mining")]
    [SerializeField] private float miningRadius = 1.2f;
    [SerializeField] private Vector2 miningOffset = new Vector2(0f, 0.8f);
    [SerializeField] private LayerMask miningLayer;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Mine.performed += _ => TryMine();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void TryMine()
    {
        Vector2 origin = (Vector2)transform.position + miningOffset;

        Collider2D hit = Physics2D.OverlapCircle(origin, miningRadius, miningLayer);
        if (hit == null) return;

        if (hit.TryGetComponent<Stalagmite>(out var stalagmite))
        {
            stalagmite.Mine(1);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position + (Vector3)miningOffset;
        Gizmos.DrawWireSphere(pos, miningRadius);
    }
#endif
}
