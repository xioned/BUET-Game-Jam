using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthChangeEvent))]
[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    [SerializeField] private int defaultHealth;
    public int currentHealth { get; private set; }
    HealthChangeEvent healthChangeEvent;

    private void Awake()
    {
        healthChangeEvent = GetComponent<HealthChangeEvent>();
    }
    private void Start()
    {
        currentHealth = defaultHealth;
    }

    public void IncreaseHealth(int AddHealthAmount)
    {
        currentHealth = currentHealth + AddHealthAmount;

        if (currentHealth > defaultHealth)
        {
            currentHealth = defaultHealth;
        }
        healthChangeEvent.CallHealthChangeEvent(currentHealth,defaultHealth);
    }
    
    public void DecreaseHealth(int RemoveHealthAmount)
    {
        currentHealth = currentHealth - RemoveHealthAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        healthChangeEvent.CallHealthChangeEvent(currentHealth,defaultHealth);
    }
}
