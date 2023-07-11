using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SlimeAI : MonoBehaviour
{
    Transform target;

    float distance;

    Animator animator;

    [Header("Movement")]
    [SerializeField] NavMeshAgent agent;
    bool moving = true;

    [SerializeField] float minimumFollowDistanceLower;
    [SerializeField] float minimumFollowDistanceUpper;
    float minimumFollowDistance;

    public float rotationDamping;

    [Header("Attack")]
    public float attackInterval;
    SlimeAttack attack;
    Collider myCollider;
    [SerializeField] ParticleSystem attackParticles;

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

            attackParticles.gameObject.SetActive(false);
            this.enabled = false;
        }
        else
        {
            animator.SetTrigger("Damage");

        }
    }
    public void HitAnimEnded()
    {
        animator.ResetTrigger("Damage");
    }

    #endregion HealthEvent

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        minimumFollowDistance = Random.Range(minimumFollowDistanceLower, minimumFollowDistanceUpper);

        StartCoroutine(MarShalare());

        attack = GetComponentInChildren<SlimeAttack>();
        attack.enabled = false;
        myCollider = GetComponent<Collider>();

    }

    private void Update()
    {
        distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.position.x, target.position.z));

        //Movement
        if (distance > minimumFollowDistance)
        {
            moving = true;
        }
        else
        {
            moving= false;
        }
        Doura();
    }

    IEnumerator MarShalare()
    {
        while (true)
        {
            if (!moving)
            {
                animator.SetTrigger("Attack");
                attack.enabled = true;
                myCollider.enabled = false;
                attackParticles.Play();
            }

            yield return new WaitForSeconds(attackInterval);
        }
    }

    public void MaraSesh()
    {
        attack.enabled = false;
        myCollider.enabled= true;
    }

    private void Doura()
    {
        if (moving)
        {
            agent.enabled = true;
            agent.destination = target.position;
            animator.SetBool("Walk", true);
        } else
        {
            agent.enabled = false;
            animator.SetBool("Walk", false);

            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
        }
    }
}
