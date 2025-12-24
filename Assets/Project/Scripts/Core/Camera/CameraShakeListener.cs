using UnityEngine;

public class CameraShakeListener : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnMiningHit += OnMiningHit;
        GameEvents.OnMiningBreak += OnMiningBreak;
    }

    private void OnDisable()
    {
        GameEvents.OnMiningHit -= OnMiningHit;
        GameEvents.OnMiningBreak -= OnMiningBreak;
    }

    private void OnMiningHit()
    {
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(ShakePreset.MiningHit);
        }
    }

    private void OnMiningBreak()
    {
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(ShakePreset.MiningBreak);
        }
    }
}
