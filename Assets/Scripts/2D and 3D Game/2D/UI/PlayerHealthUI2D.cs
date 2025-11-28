using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI2D : MonoBehaviour
{
    [SerializeField] private Player2DHealthData playerHealthData;
    [SerializeField] private GameObject heartPrefab;
    private List<Animator> hearts;

    private void OnEnable()
    {
        PlayerEvents.OnPlayerHealthChanged += HealthChange;
    }

    private void Start()
    {
        hearts = new List<Animator>();

        for (int i = 0; i < playerHealthData.MaxHealth; i++)
        {
            Animator heart = Instantiate(heartPrefab, transform).GetComponent<Animator>();
            hearts.Add(heart);
        }
    }

    void HealthChange(int healthChange)
    {
        if (healthChange > 0)
        {
            HealthGainAnimation(healthChange);
        }
        else
        {
            HealthLossAnimation(healthChange);
        }
    }

    private void HealthLossAnimation(int _healthChange)
    {

        int healthChange = Mathf.Abs(_healthChange);
        healthChange = Mathf.Min(healthChange, hearts.Count);

        for (int i = hearts.Count - 1; i >= 0 && healthChange > 0; i--)
        {
            if (!hearts[i].GetBool("Depleted"))
            {
                hearts[i].SetBool("Depleted", true);
                healthChange--;
            }
        }
    }

    private void HealthGainAnimation(int _healthChange)
    {
        int healthChange = Mathf.Abs(_healthChange);
        healthChange = Mathf.Min(healthChange, hearts.Count);

        for (int i = 0; i < hearts.Count && healthChange > 0; i++)
        {
            if (hearts[i].GetBool("Depleted"))
            {
                hearts[i].SetBool("Depleted", false);
                healthChange--;
            }
        }
    }

    private void OnDisable()
    {
        PlayerEvents.OnPlayerHealthChanged -= HealthChange;
    }
}
