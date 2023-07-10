using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Boss : MonoBehaviour
{
    [SerializeField] private float bossGotoPositionArea;
    [SerializeField] private int gotoPositionAmount;
    [SerializeField] private int gotoPositionHeight;

    [SerializeField] private Vector3[] gotoPosition;
    [SerializeField] private Vector3 nextGotoPositionArea;
    public Transform targetTransform;
    [Header("Boss")]
    public float bossRotateSpeed;
    public GameObject attack1Prefab;
    public Transform attack1Position;
    public ParticleSystem attack1ProjectileCreate;
    public MultiAimConstraint attack1HandConstraint;
    public float attack1force;
    Animator animator;
    private void Start()
    {
        CreateNewGotoPositionArray();
    }

    private void Update()
    {
        RotateTowardPlayer();
        if (Input.GetKeyDown(KeyCode.P))
        {
            CreateNewGotoPositionArray();
        }
    }

    private void CreateNewGotoPositionArray()
    {
        gotoPosition = new Vector3[gotoPositionAmount];
        for (int i = 0; i < gotoPositionAmount; i++)
        {
            float offsetX = transform.position.x - (bossGotoPositionArea / 2);
            float offsetY = transform.position.y - (bossGotoPositionArea / 2);
            gotoPosition[i] = new Vector3(UnityEngine.Random.Range(offsetX, bossGotoPositionArea), gotoPositionHeight, UnityEngine.Random.Range(offsetY, bossGotoPositionArea));
        }
    }

    private void RotateTowardPlayer()
    {
        Vector3 targetDirection = targetTransform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, bossRotateSpeed * Time.deltaTime);
    
        transform.rotation = playerRotation;
    }


    private void Attack1()
    {
        animator.SetBool("Attack1", true);
    }

    private void Attack1CreateProjectile()
    {
        attack1ProjectileCreate.gameObject.SetActive(true);
        attack1ProjectileCreate.Play();
    }

    private void Attack1ThrowProjectile()
    {
        attack1ProjectileCreate.gameObject.SetActive(false);
        Vector3 targetDirection = targetTransform.position - attack1Position.position;
        Quaternion quaternion = Quaternion.LookRotation(targetDirection);
        GameObject attack1 =  Instantiate(attack1Prefab, attack1Position.position, quaternion);
        attack1.GetComponent<Rigidbody>().AddForce(attack1.transform.forward*attack1force);
    }
    
    public void Attack2()
    {

    }
}
