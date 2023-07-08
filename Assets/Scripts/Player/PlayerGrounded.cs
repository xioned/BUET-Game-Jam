using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    Player player;
    [SerializeField] private LayerMask groundLayer;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == groundLayer)
        {
            player.other = other;
        }
        else
        {
            player.other = null;
        }
    }
}
