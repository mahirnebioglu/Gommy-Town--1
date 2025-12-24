using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow Settings")]
    [SerializeField] private float smoothTime = 0.15f;
    [SerializeField] private Vector2 offset = Vector2.zero;

    [Header("Clamp Settings (Optional)")]
    [SerializeField] private BoxCollider2D worldBounds;

    private Vector3 velocity = Vector3.zero;
    private bool initialized = false;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        if (target == null) return;

        // 🔒 OYUN BAŞINDA KAMERAYI PLAYER'A KİLİTLE
        Vector3 startPos = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );

        transform.position = startPos;
        velocity = Vector3.zero;
        initialized = true;
    }

    private void LateUpdate()
    {
        if (!initialized || target == null)
            return;

        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );

        Vector3 smoothPosition = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        // Clamp sadece smooth'tan SONRA ve sadece initialized ise
        if (worldBounds != null)
        {
            Bounds bounds = worldBounds.bounds;

            float camHeight = cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            smoothPosition.x = Mathf.Clamp(
                smoothPosition.x,
                bounds.min.x + camWidth,
                bounds.max.x - camWidth
            );

            smoothPosition.y = Mathf.Clamp(
                smoothPosition.y,
                bounds.min.y + camHeight,
                bounds.max.y - camHeight
            );
        }

        transform.position = smoothPosition;
    }
}
