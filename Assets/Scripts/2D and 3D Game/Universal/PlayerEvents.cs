using System;

public static class PlayerEvents
{
    public static event Action<int> OnPlayerHealthChanged;
    public static event Action<string> OnCollectedHealthBerry;
    public static event Action OnPlayerHit;
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerFinish;

    public static void HealthChanged(int healthChange) => OnPlayerHealthChanged?.Invoke(healthChange);

    public static void CollectedHealthBerry(string healthBerryId) => OnCollectedHealthBerry?.Invoke(healthBerryId);

    public static void Hit() => OnPlayerHit?.Invoke();

    public static void Death() => OnPlayerDeath?.Invoke();

    public static void Finish() => OnPlayerFinish?.Invoke();
}
