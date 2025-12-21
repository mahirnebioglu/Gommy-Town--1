using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        originalPosition = transform.localPosition;
    }

    public void Shake(float duration, float strength)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(duration, strength));
    }

    private IEnumerator ShakeRoutine(float duration, float strength)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector2 randomOffset = Random.insideUnitCircle * strength;
            transform.localPosition = originalPosition + new Vector3(randomOffset.x, randomOffset.y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
