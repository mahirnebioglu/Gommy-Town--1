using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugOverlayController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text fpsText;
    [SerializeField] private TMP_Text staminaText;

    [Header("References")]
    [SerializeField] private FPSCounter fpsCounter;
    [SerializeField] private PlayerStats playerStats;

    private bool _isVisible = true;

    private void Awake()
    {
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
        gameObject.SetActive(false);
#endif
    }

    private void Update()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        HandleToggle();
        UpdateTexts();
#endif
    }

    private void HandleToggle()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.f3Key.wasPressedThisFrame)
        {
            _isVisible = !_isVisible;
            gameObject.SetActive(_isVisible);
        }
    }

    private void UpdateTexts()
    {
        if (fpsCounter != null && fpsText != null)
        {
            fpsText.text = $"FPS: {Mathf.RoundToInt(fpsCounter.CurrentFPS)}";
        }

        if (playerStats != null && staminaText != null)
        {
            staminaText.text = $"Stamina: {playerStats.CurrentStamina}";
        }
    }
}
