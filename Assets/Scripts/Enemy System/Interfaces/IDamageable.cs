using UnityEngine;

public interface IDamageable
{
    float currentHealth { get; set; }

    void TakeDamage(int damageAmount);

    void Heal(int healAmount);

}
