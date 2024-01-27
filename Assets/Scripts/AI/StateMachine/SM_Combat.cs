using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Combat : StateMachine
{
    private Rigidbody target;

    public SM_Combat(SM_Settings settings) : base(settings)
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


        base.Enter();
    }


    protected override void Update()
    {
        //Engage with target



        base.Update();
    }
    protected override void Exit() { base.Exit();}
}
