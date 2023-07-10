using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEvent : MonoBehaviour
{
    public event Action OnDestroyEvent;

    public void CallDestroyEvent()
    {
        OnDestroyEvent?.Invoke();
    }
}
