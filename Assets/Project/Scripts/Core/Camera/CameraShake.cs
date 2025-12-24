using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [System.Serializable]
    public class ShakePresetData
    {
        public ShakePreset Preset;
        public float Duration = 0.05f;
        public float Strength = 0.1f;
    }

    [Header("Presets")]
    [SerializeField] private List<ShakePresetData> presets = new();

    [Header("Smoothing")]
    [SerializeField] private float smoothReturnSpeed = 20f;

    private Vector3 originalLocalPosition;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        originalLocalPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        // Shake bittikten sonra kamerayı yumuşakça yerine döndür
        if (shakeRoutine == null)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalLocalPosition,
                Time.deltaTime * smoothReturnSpeed
            );
        }
    }

    // 🔹 Dışarıdan preset ile çağırılan ANA METHOD
    public void Shake(ShakePreset preset)
    {
        ShakePresetData data = presets.Find(p => p.Preset == preset);

        if (data == null)
        {
            Debug.LogWarning($"[CameraShake] Preset bulunamadı: {preset}");
            return;
        }

        Shake(data.Duration, data.Strength);
    }

    // 🔹 Low-level shake (duration + strength)
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
            Vector2 randomOffset = Random.insideUnitCircle * strength;
            transform.localPosition = originalLocalPosition + new Vector3(randomOffset.x, randomOffset.y, 0f);


            elapsed += Time.deltaTime;
            yield return null;
        }

        shakeRoutine = null;
    }
}
