using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float patrolRadius = 10f;
    public float agroRadius = 10f;
    public float attackRadius = 3f;
    public LayerMask playerMask;
    public float minPatrolTime = 3f, maxPatrolTime = 6f;
    private NavMeshAgent agent;
    private EnemyMotor motor;
    private EnemyAnimator anim;
    private Transform target;

    private bool isPatrolling = false;
    private bool isAgroed = false;
    private bool isAttacking = false;
    private float AttackRate = 1f;
    private float NextAttack = 1f;

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<EnemyAnimator>();
        motor = GetComponent<EnemyMotor>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!isAgroed)
        {
            CheckAgro();
            if(!isPatrolling && !isAgroed)
            {
                StartCoroutine(ChoosePatrolLocation());
            }
        }
        else
        {
            FindTarget();
            motor.move(target.position);
            // if(target.GetComponent<playerController>().IsDead)
            // {
            //     return;
            // }
            if(CloseEnoughToAttack() && !isAttacking && Time.time > NextAttack)
            {
                isAttacking = true;
                NextAttack = Time.time + AttackRate;
                motor.stop();
                anim.setTriggerValue("Attack");
            }
        }
    }

    public void FindTarget()
    {
        // Debug.Log("in target");
        foreach(Collider item in Physics.OverlapSphere(transform.position + transform.forward * attackRadius, attackRadius, playerMask))
        {
            if(item.transform.CompareTag("Player"))
            {
                // Debug.Log("found player");
                // bool x = item.transform.GetComponent<playerController>().IsDead;
                motor.setTarget(item.transform);
                target = item.transform;
            }
        }
    }

    private void CheckAgro()
    {
        foreach(Collider col in Physics.OverlapSphere(transform.position, agroRadius, playerMask))
        {
            if(col.transform.CompareTag("Player"))
            {
                motor.setTarget(col.transform);
                target = col.transform;
                isAgroed = true;
            }
        }
    } 


    IEnumerator ChoosePatrolLocation()
    {
        if(!isAgroed)
        {
            isPatrolling = true;

            Vector3 offset = Random.insideUnitSphere * patrolRadius;
            offset.y = 0;
            offset += transform.position;
            motor.move(offset);

            yield return new WaitForSeconds(Random.Range(minPatrolTime, maxPatrolTime));
            StartCoroutine(ChoosePatrolLocation());
        }
    }

    private bool CloseEnoughToAttack()
    {
        return Vector3.Magnitude(transform.position - target.position) <= agent.stoppingDistance;
    }

    void Attack()
    {
        Debug.Log("in attack animation");
        foreach(Collider item in Physics.OverlapSphere(transform.position + transform.forward * attackRadius, attackRadius, playerMask))
        {
            if(item.transform.CompareTag("Player"))
            {
                // playerController player = item.GetComponent<playerController>();
                // player.reduceHealth(10);
                Debug.Log("attacking");
                resetAttack();
            }
        }
    }

    void resetAttack()
    {
        isAttacking = false;
    }
}
