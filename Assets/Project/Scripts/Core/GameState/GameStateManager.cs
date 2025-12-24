using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetState(GameState.Boot);
        SetState(GameState.Playing);
    }

    public void SetState(GameState newState)
    {
        Debug.Log($"Exit State: {CurrentState}");
        CurrentState = newState;
        Debug.Log($"Enter State: {CurrentState}");
    }

    // =========================
    // A.5.4 – GLOBAL RESET
    // =========================
    public void ResetRun()
    {
        Debug.Log("RESET RUN");

        // 1. Tüm sistemlere haber ver
        GameEvents.RunReset?.Invoke();

        // 2. State’i tekrar Playing yap
        SetState(GameState.Playing);
    }
}
