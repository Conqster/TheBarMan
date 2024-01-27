using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class SM_Drinking : StateMachine
{

    private float m_drinkingDuration = 3.0f;
    private float m_rotationSpeed = 5.0f;
    private Vector3 m_lookAt;

    public SM_Drinking(SM_Settings settings, BrainOutput output, Vector3 lookAt) : base(settings, output)
    {
        sm_name = "Idle State";
        sm_event = SM_Event.Enter;
        sm_duration = 0.0f;
        sm_state = SM_State.Drinking;
        m_lookAt = lookAt;
    }

    protected override void Enter()
    {

        //rotates towards player
        Vector3 dir = m_lookAt - sm_settings.transform.position; 
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        sm_settings.transform.rotation = Quaternion.Slerp(sm_settings.transform.rotation, lookRotation, m_rotationSpeed * Time.deltaTime);


        sm_output.drinking = true;
        m_drinkingDuration = sm_settings.drinkDuration;

        sm_settings.agent.isStopped = true;

        base.Enter();
    }


    protected override void Update()
    {
        Vector3 dir = m_lookAt - sm_settings.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        sm_settings.transform.rotation = Quaternion.Slerp(sm_settings.transform.rotation, lookRotation, m_rotationSpeed * Time.deltaTime);


        if (sm_duration > m_drinkingDuration)
            TriggerExit(new SM_Combat(sm_settings, sm_output));

        base.Update();
    }

    protected override void Exit()
    {
        sm_output.drinking = false;
        sm_settings.agent.isStopped = false;
        base.Exit();
    }



}
