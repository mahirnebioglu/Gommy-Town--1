using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow Settings")]
    [SerializeField] private float smoothTime = 0.15f;
    [SerializeField] private Vector2 offset;

    [Header("Clamp Settings (Optional)")]
    [SerializeField] private BoxCollider2D worldBounds;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z // 🔴 Z ASLA DEĞİŞMEZ
        );

        Vector3 smoothPosition = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        if (worldBounds != null)
        {
            Bounds bounds = worldBounds.bounds;

            float camHeight = Camera.main.orthographicSize;
            float camWidth = camHeight * Camera.main.aspect;

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
