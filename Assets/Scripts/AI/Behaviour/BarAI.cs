using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BarAI : MonoBehaviour
{
    private StateMachine barAI_SM;


    [SerializeField] private HealthSystem healthSystem;

    [Header("State Machine Input Data")]
    [SerializeField] private SM_Settings brainSetting;

    [Header("State Machine Output Data")]
    [SerializeField] private StateMachineData SMData;
    [SerializeField] private BrainOutput SMBrainOutput;


    // Start is called before the first frame update
    void Start()
    {
        barAI_SM = new SM_Idle(brainSetting, SMBrainOutput);
    }

    // Update is called once per frame
    void Update()
    {
        barAI_SM = barAI_SM.Process();
        SMData = barAI_SM.GetStateMachineData();
    }


    private void LateUpdate()
    {
        ProcessHealth();
    }


    public SM_State GetState()
    { return barAI_SM.GetStateMachineData().state; }

    public BrainOutput GetBrainOutput() { return SMBrainOutput; }

    public HealthSystem GetHealthSystem() { return healthSystem; }


    private void ProcessHealth()
    {
        if(healthSystem != null)
            if(healthSystem.currentHealth <= 0.0f)
            {
                Destroy(this.gameObject);
                //brainSetting.ACoord.RemoveMe(this);
            }
    }
}
