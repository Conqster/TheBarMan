using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float speed;
    public float attackPower;
    public float defense;
    public float sobrietyLevel;

    public void ModifySpeed(float amount) { speed += amount; }
    public void ModifyAttackPower(float amount) { attackPower += amount; }
    public void ModifyDefense(float amount) { defense += amount; }
    public void ModifySobrietyLevel(float amount) { sobrietyLevel += amount; }
}
