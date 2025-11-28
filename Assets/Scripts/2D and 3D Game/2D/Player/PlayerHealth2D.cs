using System;
using UnityEngine;

public class PlayerHealth2D : MonoBehaviour, IDamageable2D
{
    [SerializeField] private Player2DHealthData playerHealthData;
    public int currentHealth { get; set; }

    private void Start()
    {
        currentHealth = playerHealthData.MaxHealth;
    }

    public void HealthChange(int healthChange)
    {
        PlayerEvents.HealthChanged(healthChange);

        currentHealth += healthChange;

        if (currentHealth <= 0)
        {
            PlayerEvents.Death();
        }
    }
}
