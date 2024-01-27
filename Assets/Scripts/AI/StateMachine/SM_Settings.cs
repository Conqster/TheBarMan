using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class SM_Settings
{
    [Header("IdleState")]
    [SerializeField, Range(0.0f, 10.0f)] public float idleDuration = 2.0f;


    [Header("WalkAround Data")]
    [SerializeField] public EnvQuerySys levelEQS;


    [Header("General")]
    [SerializeField] public NavMeshAgent agent;

}
