using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerHealthManager : MonoBehaviour
{
    public ParticleSystem particles;

    HealthUpdateEvent healthUpdateEvent;

    public UnityEvent onDeath;
    

    private void Awake()
    {
        healthUpdateEvent = GetComponent<HealthUpdateEvent>();
    }

    private void OnEnable()
    {
        healthUpdateEvent.OnHealthUpdateEvent += OnHealthUpDate;
    }

    private void OnHealthUpDate(float currentHealth, float defaultHealth)
    {
        if (currentHealth <= 0)
        {

            particles.Play();
            onDeath.Invoke();
            Destroy(gameObject);
        }
        else
        {

        }
    }
}
