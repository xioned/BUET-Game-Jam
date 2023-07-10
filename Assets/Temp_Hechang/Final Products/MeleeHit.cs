using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().DecreaseHealth((int)damage);
        }
    }
}
