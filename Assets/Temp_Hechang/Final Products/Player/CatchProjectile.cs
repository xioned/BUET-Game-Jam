using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CatchProjectile : MonoBehaviour
{
    public Projectile.Projectile_Color equipedColor;
    bool catching;
    bool caught = false;
    [SerializeField] Rig handRig;
    public float weightValue = 0;

    Projectile projectile;

    [SerializeField] ParticleSystem blueParticles;
    [SerializeField] ParticleSystem redParticles;

    [SerializeField] float catchingTime;
    float timer = 0;

    public Transform ProjectilePoint;

    public GameObject[] additionalChecks;

    private void Start()
    {
        Math.Clamp(weightValue, 0.0f, 1.0f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !catching && !caught)
        {
            //Begin Catch Sequence
            catching = true;
            if(equipedColor == Projectile.Projectile_Color.Blue)
            {
                blueParticles.Play();
            }
            else
            {
                redParticles.Play();
            }
            StartCoroutine(HatUtha());

            foreach (GameObject obj in additionalChecks)
            {
                obj.SetActive(true);
            }

        }

        if(Input.GetMouseButton(1)&& caught)
        {
            ChargeProjectile();
        } else if(Input.GetMouseButton(1)&& catching && !caught)
        {
            timer += Time.deltaTime;
            if(timer >= catchingTime)
            {
                CatchingEnded();
            }
        }

        if(Input.GetMouseButtonUp(1)&& caught)
        {
            ThrowProjectile();
        } else if(Input.GetMouseButtonUp(1) && !caught && catching)
        {
            CatchingEnded();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !catching && !caught)
        {
            SwapColor();
        }
    }

    private void ThrowProjectile()
    {
        //Throw The Projectile
        caught = false;
        catching = false;
        StopAllCoroutines();
        StartCoroutine(HatNama());
        timer = 0;

        projectile.ThrewAgain(timer);
        projectile.GetComponent<Rigidbody>().isKinematic = false;

        //CHANGE THIS CODE TO THROW TOWARDS THE CURSOr
        //I REPEAT
        //Look
        //THIS IS THE CODE
        projectile.GetComponent<Rigidbody>().AddForce(ProjectilePoint.forward * 10, ForceMode.VelocityChange);
        projectile = null;
    }

    private void ChargeProjectile()
    {
        timer += Time.deltaTime;
    }

    IEnumerator HatUtha()
    {
        while(weightValue <= 1)
        {
            weightValue += 0.1f;
            handRig.weight = weightValue;

            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator HatNama()
    {
        while (weightValue >= 0)
        {
            weightValue -= 0.1f;
            handRig.weight = weightValue;

            yield return new WaitForSeconds(0.02f);
        }
    }

    private void SwapColor()
    {
        if(equipedColor == Projectile.Projectile_Color.Red)
        {
            equipedColor = Projectile.Projectile_Color.Blue;
        }
        else
        {
            equipedColor = Projectile.Projectile_Color.Red;
        }
    }

    public void CatchingEnded()
    {
        catching = false;
        timer = 0;
        StopAllCoroutines();
        StartCoroutine(HatNama());

        foreach (GameObject obj in additionalChecks)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && !caught &&catching)
        {
           caught = other.GetComponent<Projectile>().TryToGrab(equipedColor, ProjectilePoint);
            if (caught)
            {
                projectile = other.GetComponent<Projectile>();
                timer = 0;

                foreach (GameObject obj in additionalChecks)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    public void RecievedFromAnother(Projectile _projectile)
    {
        if (!caught && catching)
        {
            caught = _projectile.TryToGrab(equipedColor, ProjectilePoint);
            if (caught)
            {
                projectile = _projectile;
                timer = 0;

                foreach(GameObject obj in additionalChecks)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
