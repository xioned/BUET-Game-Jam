using System;
using UnityEngine;

public class HealthUpdateEvent : MonoBehaviour
{
    public event Action<float, float> OnHealthUpdateEvent;

    public void CallHealthUpdateEvent(float currentHealth, float defaultHealth)
    {
        OnHealthUpdateEvent?.Invoke(currentHealth, defaultHealth);
    }
}
