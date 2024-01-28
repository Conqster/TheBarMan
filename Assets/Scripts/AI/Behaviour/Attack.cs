using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BarAI_Anim barAI_Anim;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Character"))
        {
            if (other.TryGetComponent<BarAI>(out BarAI attackeeAI))
                barAI_Anim.AttackDamage(attackeeAI);
            if (other.TryGetComponent<PlayerStats>(out PlayerStats playerStats))
                barAI_Anim.AttackDamage(playerStats);
        }
    }
}
