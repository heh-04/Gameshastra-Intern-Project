using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI2D : MonoBehaviour
{
    [SerializeField] private Player2DHealthData playerHealthData;
    [SerializeField] private GameObject heartPrefab;
    private List<Animator> hearts;

    private void OnEnable()
    {
        PlayerEvents.OnPlayerHealthChanged += OnHealthChange;
        PlayerEvents.OnCollectedHealthBerry += OnCollectedHealthBerry;
    }

    private void Start()
    {
        hearts = new List<Animator>();
        AddUIHearts(playerHealthData.StartingHealth, false);
    }

    public void OnCollectedHealthBerry(string healthBerryId)
    {
        AddUIHearts(1, true);
    }

    void OnHealthChange(int healthChange)
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

    private void AddUIHearts(int number, bool depleted)
    {
        for (int i = 0; i < number; i++)
        {
            Animator heart = Instantiate(heartPrefab, transform).GetComponent<Animator>();
            heart.SetBool("Depleted", depleted);
            hearts.Add(heart);
        }
    }

    private void OnDisable()
    {
        PlayerEvents.OnPlayerHealthChanged -= OnHealthChange;
        PlayerEvents.OnCollectedHealthBerry -= OnCollectedHealthBerry;
    }
}
