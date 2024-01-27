using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SM_WalkAround : StateMachine
{

    private List<Vector3> points = new List<Vector3>();

    public SM_WalkAround(SM_Settings setting) : base(setting)
    {
        sm_name = "WalkAround State";
        sm_event = SM_Event.Enter;
        sm_duration = 0.0f;
        sm_state = SM_State.WalkingAround;
    }

    protected override void Enter()
    {
        Dictionary<Vector3, bool> tempQuery = sm_settings.levelEQS.HardQueryPosts();


        foreach (var q in tempQuery)
        {
            if (q.Value)
            {
                points.Add(q.Key);
            }
        }

        ChooseLocation();
        //sm_settings.agent.ResetPath();
        base.Enter();
    }


    protected override void Update()
    {
        //if (!sm_settings.agent.hasPath)
        //    ChooseLocation();

        if (sm_settings.agent.remainingDistance < 2.0f)
        {
            //sm_settings.agent.ResetPath();
            TriggerExit(new SM_Idle(sm_settings));
        }

        base.Update();
    }

    protected override void Exit() 
    {
        //if (sm_settings.agent.hasPath)
        //{
        //    sm_settings.agent.ResetPath();
        //}
        base.Exit(); 
    }



    void ChooseLocation()
    {
        //points = m_PostSelector.QueryWalkablePosts();

        Debug.Log("Count: " + points.Count);
        int rnd = Random.Range(0, points.Count - 1);
        Debug.Log("Rnd Value: " + rnd);
        //m_Agent.destination = points[rnd];
        sm_settings.agent.SetDestination(points[rnd]);
    }
}
