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
    [SerializeField, Range(0.0f, 5.0f)] public float walkSpeed = 2.0f;

    [Header("Drink")]
    [SerializeField, Range(0.0f, 5.0f)] public float drinkDuration = 2.0f;

    [Header("Combat Data")]
    [SerializeField, Range(0.0f, 20.0f)] public float visionLength = 2.0f;
    [SerializeField, Range(0.0f, 15.0f)] public float agentStoppingDistance = 0.2f;
    [SerializeField, Range(0.0f, 15.0f)] public float attackDistance = 0.3f;
    [SerializeField, Range(0.0f, 5.0f)] public float moveSpeed = 2.0f;


    [Header("General")]
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Rigidbody rb;
    [SerializeField] public Transform transform;
    [SerializeField] public Transform child;
    [SerializeField] public AttackCoordinator ACoord;
    [SerializeField] public BarAI barAI;
    [SerializeField] public Rigidbody player;

}


[System.Serializable]   
public class BrainOutput
{
    [SerializeField] public bool walking = false;
    [SerializeField] public bool attacking = false;
    [SerializeField] public bool inCombat = false;
    [SerializeField] public bool drinking = false;
    [SerializeField] public float attackCooldown = 3.0f;
    [SerializeField] public bool canAttackPlayer;
    [SerializeField] public Rigidbody currentTarget;
}
