using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Idle : StateMachine
{
    private float m_IdleDuration = 3.0f;

    public SM_Idle(SM_Settings setting) : base(setting)
    {
        sm_name = "Idle State";
        sm_event = SM_Event.Enter;
        sm_duration = 0.0f;
        sm_state = SM_State.Idle;
    }

    protected override void Enter()
    {
        m_IdleDuration = sm_settings.idleDuration;



        base.Enter();
    }


    protected override void Update()
    {
        if (sm_duration > m_IdleDuration)
            TriggerExit(new SM_WalkAround(sm_settings));


        base.Update();
    }

    protected override void Exit() { base.Exit();}
}
