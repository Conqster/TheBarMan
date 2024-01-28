using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SM_WalkAround : StateMachine
{

    private List<Vector3> points = new List<Vector3>();
    private List<Rigidbody> enemies = new List<Rigidbody>();

    private float drinkingDecision = 1.0f;
    private float detectTimer = 0.0f;

    public SM_WalkAround(SM_Settings setting, BrainOutput output) : base(setting, output)
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
        sm_output.walking = true;


        drinkingDecision = Random.Range(0.5f, 2.0f);

        //Debug.Log("Drinking decision: " + drinkingDecision);

        base.Enter();
    }


    protected override void Update()
    {
        //if (!sm_settings.agent.hasPath)
        //    ChooseLocation();

        enemies = Perception.Vision(sm_settings.rb, sm_settings.visionLength);

        if (enemies.Count > 0)
            detectTimer += Time.deltaTime;
        else
            detectTimer = 0.0f;




        //probalbly change to player 
        if(detectTimer > drinkingDecision)
        {
            if (sm_settings.ACoord.CanIAttack(sm_settings.barAI))
            {
                TriggerExit(new SM_Drinking(sm_settings, sm_output, sm_settings.player.position));
            }
            else
                TriggerExit(new SM_Drinking(sm_settings, sm_output, ClosestTarget(enemies).position));
        }


        //if (sm_settings.agent.remainingDistance < 2.0f)
        //{
        //    //sm_settings.agent.ResetPath();
        //    TriggerExit(new SM_Idle(sm_settings, sm_output));
        //}

        if(!sm_settings.agent.hasPath)
            TriggerExit(new SM_Idle(sm_settings, sm_output));

        base.Update();
    }

    protected override void Exit() 
    {
        sm_settings.ACoord.RemoveMe(sm_settings.barAI);
        sm_output.walking = false;
        base.Exit(); 
    }



    void ChooseLocation()
    {
        //points = m_PostSelector.QueryWalkablePosts();

        //Debug.Log("Count: " + points.Count);
        int rnd = Random.Range(0, points.Count - 1);
        //Debug.Log("Rnd Value: " + rnd);
        //m_Agent.destination = points[rnd];
        sm_settings.agent.SetDestination(points[rnd]);
    }


}
