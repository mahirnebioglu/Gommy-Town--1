using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [Header("Shake Settings")]
    [SerializeField] private float defaultDuration = 0.08f;
    [SerializeField] private float defaultStrength = 0.12f;

    private Vector3 originalLocalPos;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        originalLocalPos = transform.localPosition;
    }

    public void Shake()
    {
        Shake(defaultDuration, defaultStrength);
    }

    public void Shake(float duration, float strength)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeCoroutine(duration, strength));
    }

    private IEnumerator ShakeCoroutine(float duration, float strength)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector2 offset = Random.insideUnitCircle * strength;
            transform.localPosition = originalLocalPos + new Vector3(offset.x, offset.y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalLocalPos;
        shakeRoutine = null;
    }
}
