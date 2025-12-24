using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public float CurrentFPS { get; private set; }

    private float _deltaTime;

    private void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        CurrentFPS = 1f / _deltaTime;
    }
}
