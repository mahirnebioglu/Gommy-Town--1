using System;
using UnityEngine;

public static class GameEvents
{
    // =========================
    // MINING
    // =========================
    public static Action OnMiningHit;
    public static Action OnMiningBreak;

    // =========================
    // GAME FLOW
    // =========================
    public static Action RunReset;

    // =========================
    // HELPERS
    // =========================
    public static void RaiseMiningHit()
    {
        OnMiningHit?.Invoke();
    }

    public static void RaiseMiningBreak()
    {
        OnMiningBreak?.Invoke();
    }

    public static void RaiseRunReset()
    {
        RunReset?.Invoke();
    }
}
