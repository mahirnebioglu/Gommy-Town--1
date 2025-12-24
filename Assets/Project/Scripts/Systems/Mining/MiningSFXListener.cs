using UnityEngine;

public class MiningSFXListener : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource hitSource;
    [SerializeField] private AudioSource breakSource;

    [Header("Clips")]
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip breakClip;

    private void OnEnable()
    {
        GameEvents.OnMiningHit += PlayHit;
        GameEvents.OnMiningBreak += PlayBreak;
    }

    private void OnDisable()
    {
        GameEvents.OnMiningHit -= PlayHit;
        GameEvents.OnMiningBreak -= PlayBreak;
    }

    private void PlayHit()
    {
        if (hitSource && hitClip)
            hitSource.PlayOneShot(hitClip);
    }

    private void PlayBreak()
    {
        if (breakSource && breakClip)
            breakSource.PlayOneShot(breakClip);
    }
}
