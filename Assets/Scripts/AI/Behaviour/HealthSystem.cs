using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthSystem
{
    [Header("Self")]
    [SerializeField, Range(0.0f, 10.0f)] public float currentHealth;
    [SerializeField, Range(0.0f, 10.0f)] public float maxHealth;


    [Header("Attakee")]
    [SerializeField, Range(0.0f, 10.0f)] public float damageValue;
    [SerializeField, Range(0.0f, 10.0f)] public float playerDamage;
    


    public void DealDamage(float damage)
    {

        if(currentHealth > 0)
            currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0.0f;
    }

}
