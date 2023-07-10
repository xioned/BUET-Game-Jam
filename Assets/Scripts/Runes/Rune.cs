using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DestroyEvent))]
public class Rune : MonoBehaviour
{
    Health health;
    DestroyEvent destroyEvent;
    [SerializeField] private GameObject[] runeDestroyedParts;
    [SerializeField] private GameObject[] objectsToActive;
    [SerializeField] private GameObject baseRune;
    private void Awake()
    {
        destroyEvent = GetComponent<DestroyEvent>();
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        destroyEvent.OnDestroyEvent += DestroyThisRune;
    }

    private void DestroyThisRune()
    {
        foreach (GameObject _gameObject in runeDestroyedParts)
        {
            _gameObject.SetActive(true);
            Destroy(_gameObject, 5f);
            _gameObject.transform.parent = null;
        }

        foreach (GameObject gameObject in objectsToActive)
        {
            gameObject.SetActive(true);
            gameObject.transform.parent = null;
        }

        
        Destroy(baseRune);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.DecreaseHealth(3);
        }
    }
}
