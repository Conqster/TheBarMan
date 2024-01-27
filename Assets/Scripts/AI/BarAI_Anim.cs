using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarAI_Anim : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SM_State state;

    [SerializeField] private BrainOutput output;
    [SerializeField] private BarAI barAI;

    private Animator animator;
    private bool moving = false;

    private bool drinking = false;
    private bool engaging = false;

    private bool combatWalk = false;

    // Start is called before the first frame update
    void Start()
    {
        //state = barAI.GetState();
        
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //moving = (agent.velocity.magnitude > 0);
        moving = barAI.GetBrainOutput().walking;
        animator.SetBool("isWalking", moving);

        drinking = barAI.GetBrainOutput().drinking;
        animator.SetBool("isDrinking", drinking);


        engaging = barAI.GetBrainOutput().inCombat && barAI.GetBrainOutput().attacking;
        animator.SetBool("isAttacking", engaging);

        combatWalk = barAI.GetBrainOutput().inCombat && barAI.GetBrainOutput().walking;
        animator.SetBool("combatWalk", combatWalk);
    }


    public void MirrorAnimationState()
    {
        Vector3 currentScale = transform.localScale;

        currentScale.x = -currentScale.x;

        transform.localScale = currentScale;


        //animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);
    }
}
