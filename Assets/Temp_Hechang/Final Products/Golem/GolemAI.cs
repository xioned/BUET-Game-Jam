using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class GolemAI : MonoBehaviour
{

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
    public float minimumDistanceToSlam;

    NavMeshAgent agent;
    float distance;
    bool attacking = false;

    #region HealthEvent
    HealthUpdateEvent healthUpdateEvent;
    private void Awake()
    {
        healthUpdateEvent = GetComponent<HealthUpdateEvent>();
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
        }
        else
        {
            animator.SetTrigger("Hit");

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
    }

    private void Update()
    {
        distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.position.x, target.position.z));

        if (followMinimumDistance <= distance)
        {
            agent.enabled = true;
            agent.destination = target.position;

            animator.SetBool("Walk", true);
        }
        else if (walkMinimumDistance >= distance)
        {
            agent.enabled = false;
            animator.SetBool("Walk", false);
        }

        if(distance < minimumDistanceToSlam && !attacking)
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
        animator.SetTrigger("Slam");
        attacking = true;
        Invoke(nameof(SlamEnded), 2f);
    }

    void SlamEnded()
    {
        attacking = false;
    }

    public IEnumerator ShootProjectile()
    {
        while (true)
        {
            if (minimumDistance >= distance && !attacking && minimumDistanceToSlam < distance)
            {
                attacking = true;
                animator.SetTrigger("Attack");
                projectileCounter++;

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
        } else if( tmp_Color == Projectile.Projectile_Color.Blue)
        {
            projectileObj = Instantiate(blueProjectilePrefab, throwPoint.position, Quaternion.identity);
        }
        else
        {
            projectileObj = Instantiate(purpleProjectilePrefab, throwPoint.position, Quaternion.identity);
        }


        projectileObj.LookAt(target.position);

        projectileObj.GetComponent<Rigidbody>().AddForce(projectileObj.forward * force, ForceMode.VelocityChange);

        projectileObj.GetComponent<Projectile>().StartingJourney(damage);

        attacking = false;
    }

    
}
