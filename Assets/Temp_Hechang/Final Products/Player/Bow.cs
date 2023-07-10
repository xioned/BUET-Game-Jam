using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Bow : MonoBehaviour
{
    [SerializeField] Rig rig;

    [SerializeField] Transform arrowHand;
    Vector3 basePos;

    float weight;
    [SerializeField] float maxChargeTime;
    [SerializeField] float baseDamage;
    [SerializeField] float baseForce;
    [SerializeField] float pullLimit = 0.08f;

    GameObject tir;
    float chargeTimer;
    float finalForce;

    [Header("Charged Shot")]
    [SerializeField] int chargedShotAfter = 3;
    [SerializeField] float damageMultiplier = 2;
    int shotCounter = 0;
    [SerializeField] ParticleSystem chargedParticles;
    

    public GameObject bowPrefab;

    bool dhonukDhorchi;
    bool tirReady;

    private void Start()
    {
        Math.Clamp(weight, 0f, 1f);

        basePos = arrowHand.localPosition;

        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(DhonukUtha());
        }

        if (Input.GetMouseButton(0) && dhonukDhorchi)
        {
            DhonukChargeDe();
            tirReady = true;
        }

        if (Input.GetMouseButtonUp(0))
        {

            

            StopAllCoroutines();
            StartCoroutine(DhonukNama());
            
            if (tirReady)
            {
                TirMar();
            }

            chargeTimer = 0;
        }
    }

    private void TirMar()
    {
        Debug.Log(shotCounter);

        if (shotCounter < chargedShotAfter)
        {
            finalForce = baseForce + (baseForce * chargeTimer * 2.5f);

            tir.GetComponent<Rigidbody>().isKinematic = false;
            tir.transform.SetParent(null);
            tir.GetComponent<Rigidbody>().AddForce(arrowHand.forward * finalForce, ForceMode.VelocityChange);
            tir.GetComponent<Arrow>().ArrowShot(chargeTimer, baseDamage + baseDamage * chargeTimer, false);

            tirReady = false;
            dhonukDhorchi = false;

            shotCounter++;
        }
        else if (shotCounter == chargedShotAfter && chargeTimer < maxChargeTime)
        {
            finalForce = baseForce + (baseForce * chargeTimer * 2.5f);

            tir.GetComponent<Rigidbody>().isKinematic = false;
            tir.transform.SetParent(null);
            tir.GetComponent<Rigidbody>().AddForce(arrowHand.forward * finalForce, ForceMode.VelocityChange);
            tir.GetComponent<Arrow>().ArrowShot(chargeTimer, baseDamage + baseDamage * chargeTimer, false);

            tirReady = false;
            dhonukDhorchi = false;

        }
        else if (shotCounter == chargedShotAfter && chargeTimer >= maxChargeTime)
        {
            finalForce = baseForce + (baseForce * chargeTimer * 3f);

            tir.GetComponent<Rigidbody>().isKinematic = false;
            tir.transform.SetParent(null);
            tir.GetComponent<Rigidbody>().AddForce(arrowHand.forward * finalForce, ForceMode.VelocityChange);
            tir.GetComponent<Arrow>().ArrowShot(chargeTimer, baseDamage + baseDamage * chargeTimer * damageMultiplier, true);

            tirReady = false;
            dhonukDhorchi = false;

            shotCounter = 0;
            chargedParticles.Stop();
        }
    }

    private void DhonukChargeDe()
    {
        if(chargeTimer <= maxChargeTime)
        {
            chargeTimer += Time.deltaTime;
        }
        else
        {
            dhonukDhorchi = false;
            if(shotCounter == chargedShotAfter)
            {
                chargedParticles.Play();
            }
        }
    }

    IEnumerator DhonukUtha()
    {
        while(weight <= 1f && !tirReady)
        {
            weight += 0.1f;
            rig.weight = weight;
            if(weight >= 1f)
            {
                dhonukDhorchi = true;
                TirLaga();
                StartCoroutine(DhonukeTanDe());
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator DhonukeTanDe()
    {

        while (arrowHand.localPosition.z >= pullLimit)
        {
            arrowHand.localPosition = new Vector3(arrowHand.localPosition.x, arrowHand.localPosition.y, arrowHand.localPosition.z - 0.01f);
            yield return new WaitForSeconds(0.015f);
        }
    }

    private void TirLaga()
    {
        tir = Instantiate(bowPrefab, arrowHand);
        tir.transform.SetLocalPositionAndRotation(Vector3.zero, arrowHand.localRotation);
    }

    IEnumerator DhonukNama()
    {
        while(weight >= 0f)
        {
            weight -= 0.15f;
            rig.weight = weight;

            if(weight <= 0.05f)
            {
                arrowHand.localPosition = basePos;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
}
