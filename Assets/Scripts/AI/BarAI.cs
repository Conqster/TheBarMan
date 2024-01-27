using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BarAI : MonoBehaviour
{
    [Space]
    [SerializeField] private StateMachine barAI_SM;

    [Header("State Machine Input Data")]
    [SerializeField] private SM_Settings brainSetting;

    [Header("State Machine Output Data")]
    [SerializeField] private StateMachineData SMData;


    // Start is called before the first frame update
    void Start()
    {
        barAI_SM = new SM_Idle(brainSetting);
    }

    // Update is called once per frame
    void Update()
    {

        barAI_SM = barAI_SM.Process();
        SMData = barAI_SM.GetStateMachineData();
    }
}
