using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SM_Combat : StateMachine
{
    private Rigidbody target = null;
    private Rigidbody currentTarget = null;
    private List<Rigidbody> targetList;

    private float m_rotationSpeed = 5.0f;

    private Rigidbody player = null;
    private bool canAttackPlayer = false;

    public SM_Combat(SM_Settings settings, BrainOutput output) : base(settings, output)
    {
        sm_name = "Combat State";
        sm_event = SM_Event.Enter;
        sm_duration = 0.0f;
        sm_state = SM_State.InCombat;
    }


    protected override void Enter()
    {
        //Drink
        //Select a target
        sm_output.walking = true;
        player = sm_settings.player;

        targetList = Perception.Vision(sm_settings.rb, sm_settings.visionLength);

        target = ClosestTarget(targetList);


        if (sm_settings.ACoord.CanIAttack(sm_settings.barAI))
        {
            currentTarget = player;
            sm_output.canAttackPlayer = canAttackPlayer = true; 
            if(sm_settings.ACoord.QueryFightPos(out Vector3 pos))
                sm_settings.agent.SetDestination(pos);
        }
        else
            currentTarget = target;

        sm_settings.agent.stoppingDistance = sm_settings.agentStoppingDistance;
        sm_settings.agent.speed = sm_settings.moveSpeed;
        sm_output.inCombat = true;

        base.Enter();
    }


    protected override void Update()
    {
        //Engage with target
        if (target == null)
            TriggerExit(new SM_Idle(sm_settings, sm_output));

        if(currentTarget == null)
            currentTarget = target;

        if(currentTarget != null)
        {

            sm_output.walking = !sm_output.attacking;

            sm_output.attacking = (Vector3.Distance(currentTarget.position, sm_settings.transform.position) < sm_settings.attackDistance);
            //lookat target

            //Debug.Log("Attacking" + Vector3.Distance(target.position, sm_settings.transform.position));
            if (sm_output.attacking)
            {
                Vector3 dir = currentTarget.position - sm_settings.transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                sm_settings.transform.rotation = Quaternion.Slerp(sm_settings.transform.rotation, lookRotation, m_rotationSpeed * Time.deltaTime);
            }

            if (!sm_output.attacking)
                sm_settings.agent.destination = currentTarget.position;


            sm_output.currentTarget = currentTarget;
        }

        

        base.Update();
    }
    protected override void Exit() 
    {
        sm_output.inCombat = false;
        sm_output.walking = false;

        sm_settings.ACoord.RemoveMe(sm_settings.barAI);

        base.Exit();
    }


}
