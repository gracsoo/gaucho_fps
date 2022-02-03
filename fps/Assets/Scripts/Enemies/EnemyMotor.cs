using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMotor : MonoBehaviour
{
    public float rotationSpeed = 0.125f;
    private NavMeshAgent agent;
    private Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void move(Vector3 pos)
    {
        nullTarget();
        agent.isStopped = false;
        agent.SetDestination(pos); // tells agent to move to specified location
    }

    public void stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public void nullTarget()
    {
        target = null;
    }

    public void setTarget(Transform t)
    {
        target = t;
    }

    public void moveToTarget()
    {
        agent.destination = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            // Debug.Log("turning towards player");
            
            // causes the player to turn towards the enemy smoothly every frame
            Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position);
            Quaternion lerp = Quaternion.Lerp(transform.rotation, targetRot, rotationSpeed);
            transform.rotation = lerp;
        }
    }
}
