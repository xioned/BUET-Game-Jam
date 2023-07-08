using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    public Collider collid;

    private void OnTriggerStay(Collider other)
    {
        collid = other;
    }

    private void OnTriggerExit(Collider other)
    {
        collid = null;
    }
}
