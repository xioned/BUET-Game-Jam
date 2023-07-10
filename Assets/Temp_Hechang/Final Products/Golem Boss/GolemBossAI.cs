using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemBossAI : MonoBehaviour
{

    EnemySoundsManager soundManager;
    
    [Header("Movement Control")]
    Transform target;

    public float walkSpeed;
    public float walkMinimumDistance;
    public float followMinimumDistance;

    public float rotationDamping;

    Animator animator;

    [Header("Projectile Control")]
    public Transform blueProjectilePrefab;
    public Transform redProjectilePrefab;
    public Transform purpleProjectilePrefab;
    public Transform throwPoint;

    public float minimumDistance;
    public float timeBetweenShotsUpper;
    public float timeBetweenShotsLower;

    GolemVFXManager golemVFXManager;

    Projectile.Projectile_Color tmp_Color;

    public float force;

    [Header("Projectile Properties")]
    [SerializeField] float damage;
    [SerializeField] bool throwsBothColor;
    [SerializeField] Projectile.Projectile_Color color;
    [SerializeField] int purpleProjectileAfter;
    int projectileCounter = 0;

    [Header("GroundSlam")]
    public float closeSlamDist;
    public float farSlamDist;

    NavMeshAgent agent;
    float distance;
    bool attacking = false;

    #region HealthEvent
    HealthUpdateEvent healthUpdateEvent;
    private void Awake()
    {
        healthUpdateEvent = GetComponent<HealthUpdateEvent>();
        soundManager = GetComponent<EnemySoundsManager>();
    }

    private void OnEnable()
    {
        healthUpdateEvent.OnHealthUpdateEvent += OnHealthUpDate;
    }

    private void OnHealthUpDate(float currentHealth, float defaultHealth)
    {
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");

            this.enabled = false;

            soundManager.PlayDeath();

        }
        else
        {
            animator.SetTrigger("Hit");
            soundManager.PlayHit();
        }
    }

    public void HitAnimEnded()
    {
        animator.ResetTrigger("Hit");
    }
    #endregion HealthEvent

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        golemVFXManager = GetComponent<GolemVFXManager>();

        agent.speed = walkSpeed;
        //agent.destination = target.position;

        StartCoroutine(ShootProjectile());

        soundManager.PlayGrowl();
    }

    private void Update()
    {
        distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.position.x, target.position.z));

        if (followMinimumDistance <= distance)
        {
            agent.enabled = true;
            agent.destination = target.position;

            soundManager.PlayWalk();

            animator.SetBool("Walk", true);
        }
        else if (walkMinimumDistance >= distance)
        {
            agent.enabled = false;
            animator.SetBool("Walk", false);

            soundManager.StopWalk();
        }

        if (distance < farSlamDist && !attacking)
        {
            SlamDeShalare();
        }

        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    private void SlamDeShalare()
    {
        if(distance < closeSlamDist)
        {

            animator.SetTrigger("CloseSlam");
            attacking = true;
            Invoke(nameof(SlamEnded), 2f);
        }
        else
        {

            animator.SetTrigger("FarSlam");
            attacking = true;
            Invoke(nameof(SlamEnded), 2f);
        }

        soundManager.PlayAttack();
    }

    void SlamEnded()
    {
        attacking = false;
    }

    IEnumerator ShootProjectile()
    {
        while (true)
        {
            if (minimumDistance >= distance && !attacking && farSlamDist < distance)
            {
                attacking = true;
                animator.SetTrigger("Attack");
                projectileCounter++;

                soundManager.PlayAttack();

                if (projectileCounter == purpleProjectileAfter)
                {
                    tmp_Color = Projectile.Projectile_Color.Purple;
                    projectileCounter = 0;
                }
                else
                {
                    if (throwsBothColor)
                    {
                        if (Random.Range(1, 3) == 1)
                        {
                            tmp_Color = Projectile.Projectile_Color.Red;
                        }
                        else
                        {
                            tmp_Color = Projectile.Projectile_Color.Blue;
                        }
                    }
                    else
                    {
                        tmp_Color = color;
                    }
                }


                golemVFXManager.ChargeColor(tmp_Color);
            }
            yield return new WaitForSeconds(3.15f + Random.Range(timeBetweenShotsLower, timeBetweenShotsUpper));
        }
    }

    public void ThrowAProjectile()
    {
        animator.ResetTrigger("Attack");
        ProjectileSetUp();
    }

    void ProjectileSetUp()
    {

        Transform projectileObj;

        if (tmp_Color == Projectile.Projectile_Color.Red)
        {
            projectileObj = Instantiate(redProjectilePrefab, throwPoint.position, Quaternion.identity);
        }
        else if (tmp_Color == Projectile.Projectile_Color.Blue)
        {
            projectileObj = Instantiate(blueProjectilePrefab, throwPoint.position, Quaternion.identity);
        }
        else
        {
            projectileObj = Instantiate(purpleProjectilePrefab, throwPoint.position, Quaternion.identity);
        }


        projectileObj.LookAt(target.position);

        //CHANGE THIS CODE TO THROW TOWARDS THE CURSOr
        //I REPEAT
        //Look
        //THIS IS THE CODE
        projectileObj.GetComponent<Rigidbody>().AddForce(projectileObj.forward * force, ForceMode.VelocityChange);

        projectileObj.GetComponent<Projectile>().StartingJourney(damage);

        attacking = false;
    }


}