using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalGrabbers : MonoBehaviour
{
    public CatchProjectile catchProjectile;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
           catchProjectile.RecievedFromAnother(other.GetComponent<Projectile>());
        }
    }
}
