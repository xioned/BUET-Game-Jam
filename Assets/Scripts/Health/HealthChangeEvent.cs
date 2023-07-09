using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class HealthChangeEvent : MonoBehaviour
{
    public event Action<int, int> OnHealthChangeEvent;

    public void CallHealthChangeEvent(int currentAmount, int defaultAmount)
    {
        OnHealthChangeEvent?.Invoke(currentAmount, defaultAmount);
    }
}
