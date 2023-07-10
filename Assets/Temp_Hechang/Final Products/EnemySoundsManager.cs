using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundsManager : MonoBehaviour
{

    [SerializeField] AudioSource walkSound;

    [SerializeField] AudioSource growl;

    [SerializeField] AudioSource[] attackSound;

    [SerializeField] AudioSource[] hitSound;

    [SerializeField] AudioSource deathSound;

    bool walking = false;

    private void Start()
    {
        
    }

    public void PlayWalk()
    {
        if (walkSound == null)
        {
            return;
        }

        if (!walking)
        {
            walkSound.Play();
            walking = true;
        }
    }

    public void StopWalk()
    {
        if (walkSound == null)
        {
            return;
        }

        if (walking)
        {
            walkSound.Stop();
            walking = false;
        }
    }

    public void PlayGrowl()
    {
        if(growl != null)
            growl.Play();
    }

    public void PlayDeath()
    {
        if(deathSound != null)
        deathSound.Play();
    }

    public void PlayAttack()
    {
        if(attackSound.Length != 0)
        attackSound[Random.Range(0, attackSound.Length)].Play();
    }

    public void PlayHit()
    {
        if(hitSound.Length != 0)
        hitSound[Random.Range(0, hitSound.Length)].Play();
    }
}
