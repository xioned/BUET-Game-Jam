using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    HealthChangeEvent healthChangeEvent;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        healthChangeEvent = GetComponent<HealthChangeEvent>();
    }
    private void OnEnable()
    {
        healthChangeEvent.OnHealthChangeEvent += HealthUpdate;
    }private void OnDisable()
    {
        healthChangeEvent.OnHealthChangeEvent -= HealthUpdate;
    }

    #region Health
    private void HealthUpdate(int currentAmount, int defaultAmount)
    {
        if (currentAmount <= 0)
        {
            float RandomDeath = UnityEngine.Random.Range(0, 2);
            animator.SetFloat("Death", RandomDeath+.1f);
        }
    }

    #endregion health


    private void OnTriggerEnter(Collider other)
    {

    }
}
