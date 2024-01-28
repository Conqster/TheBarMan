using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float speed;
    public float attackPower;
    public float defense;
    public float sobrietyLevel;
    public float health;
    public Slider drunkslider;
    public void ModifySpeed(float amount) { speed += amount; print(speed); }
    public void ModifyAttackPower(float amount) { attackPower += amount; print(attackPower); }
    public void ModifyDefense(float amount) { defense += amount; print(defense); }
    public void ModifySobrietyLevel(float amount) { sobrietyLevel += amount; }

    public void ModifyHealth(float amount) { health += amount; print(health); }
    private void Start()
    {
       
    }
    private void Update()
    {
        drunkslider.value = sobrietyLevel;
    }
    public void JungleJuice(float amount)
    {
        health += amount;
        defense += amount;
        attackPower += amount;
        speed += amount;    
        sobrietyLevel += (amount + 30);
    }
    public void RandomBuff(float amount) {

        int pickedVar = Random.Range(0, 4);
          switch(pickedVar)
        {
                case 0:
                speed += amount;
                return;
                case 1:
                attackPower += amount;
                return;
                case 2:
                defense += amount;
                return;
                case 3:
                health += amount;
                return;
                case 4:

                    default: return;
        }  
            
    }

    public void takeDamage(float damage)
    {
        health = (damage - defense);
    }
}
