using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float patrolRadius = 10f;
    public float agroRadius = 5f;
    public float attackRadius = 3f;
    public LayerMask playerMask;
    public GameObject player;
    public GameObject impactEffect;
    public GameObject projectile;
    public Transform spawnFirePos;

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
            // Debug.Log("not agroed");
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
        foreach(Collider item in Physics.OverlapSphere(transform.position + transform.forward * attackRadius, attackRadius, playerMask))
        {
            if(item.transform.CompareTag("Player"))
            {
                // Debug.Log("found player");
                // bool x = item.transform.GetComponent<playerController>().IsDead;
                var targetRotation = Quaternion.LookRotation(item.transform.position - motor.GetComponent<Transform>().position);
                motor.GetComponent<Transform>().rotation = Quaternion.Slerp(motor.GetComponent<Transform>().rotation, targetRotation, 2 * Time.deltaTime);
                // motor.GetComponent<Transform>().LookAt(target);
                motor.setTarget(item.transform);
                target = item.transform;
            }
        }
    }

    private void CheckAgro()
    {
        foreach(Collider col in Physics.OverlapSphere(transform.position, agroRadius, playerMask))
        {
            RaycastHit hit;
            var rayDirection = player.GetComponent<Transform>().position - transform.position;
            if(col.transform.CompareTag("Player") && Physics.Raycast(transform.position, rayDirection, out hit, agroRadius))
            {
                if (hit.transform == player.GetComponent<Transform>())
                {                    
                    // motor.GetComponent<Transform>().rotation = Quaternion.RotateTowards(motor.GetComponent<Transform>().rotation, col.transform.rotation, 0.125f*Time.deltaTime);
                    var targetRotation = Quaternion.LookRotation(col.transform.position - motor.GetComponent<Transform>().position);
                    motor.GetComponent<Transform>().rotation = Quaternion.Slerp(motor.GetComponent<Transform>().rotation, targetRotation, 5 * Time.deltaTime);
                    
                    motor.setTarget(col.transform);
                    target = col.transform;
                    isAgroed = true;
                }
            }
        }
        // RaycastHit hit;
        // var rayDirection = player.GetComponent<Transform>().position - transform.position;
        // if(Physics.Raycast(transform.position, rayDirection, out hit, agroRadius))
        // {
        //     if (hit.transform == player.GetComponent<Transform>())
        //     {
        //         Debug.Log("find p;ayer");
        //         motor.setTarget(hit.transform);
        //         target = hit.transform;
        //         isAgroed = true;
        //     }
        // }

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
        GameObject fire = Instantiate(projectile, spawnFirePos.position, Quaternion.identity);

        fire.GetComponent<Rigidbody>().AddForce(motor.GetComponent<Transform>().forward * 8f, ForceMode.Impulse);

        fire.GetComponent<Rigidbody>().AddForce(motor.GetComponent<Transform>().up * 2f, ForceMode.Impulse);
        
        // GameObject impact = Instantiate(impactEffect, target.position, Quaternion.LookRotation(target.position.normalized));

        // Destroy(impact, 2f);
        // Destroy(fire, 2f);
    }

    void resetAttack()
    {
        isAttacking = false;
        isAgroed = false;
        isPatrolling = false;
    }
}
