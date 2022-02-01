using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private bool moving = true;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            anim.SetFloat("Speed", agent.velocity.magnitude/agent.speed);
        }
    }

    public void setTriggerValue(string arg)
    {
        anim.SetTrigger(arg);
    }
}
