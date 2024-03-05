using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCoordinator : MonoBehaviour
{
    public int numOfAttacker;
    [SerializeField, Range(1, 3)] private int maxAttacker = 2;

    [SerializeField] private List<BarAI> attackers;
    [SerializeField] private Transform player;

    [SerializeField, Range(0.0f, 10.0f)] private float fightRange = 2.0f;
    [SerializeField, Range(5.0f, 50.0f)] private float angleBtwAttacker = 15.0f;

    [Header("Debugger")]
    [SerializeField] private Color singleAttackColour = Color.yellow;
    [SerializeField] private Color twinAttackColour = Color.cyan;

    public bool CanIAttack(BarAI attacker)
    {
        if(attackers.Count >= maxAttacker)
            return false;

        if(attackers.Contains(attacker))
            return true;

        attackers.Add(attacker);
        return true;
    }

    public void RemoveMe(BarAI attacker)
    {
        if(attackers.Contains(attacker))
            attackers.Remove(attacker); 
    }


    public bool QueryFightPos(out Vector3 pos)
    {
        pos = transform.position;

        if(player == null)
            return false;

        if (attackers.Count == 1 && maxAttacker == 3)
        {
            //a position in front of player
            pos = player.position + (player.forward * fightRange);
            return true;
        }

        if ((attackers.Count == 2 && maxAttacker == 3) || (attackers.Count == 1 && maxAttacker == 2))
        {
            //angled attack
            //adjust first attacker
            pos = player.position + (Quaternion.Euler(0, angleBtwAttacker, 0) * player.forward) * fightRange;
            return true;
        }

        if ((attackers.Count == 3 && maxAttacker == 3) || (attackers.Count == 2 && maxAttacker == 2))
        {
            pos = player.position + (Quaternion.Euler(0, -angleBtwAttacker, 0) * player.forward) * fightRange;
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {

        if(player != null)
        {
            Gizmos.color = singleAttackColour;
            Vector3 pos0 = player.position + (player.forward * fightRange);
            Gizmos.DrawWireSphere(pos0, 0.5f);


            Vector3 pos1 = player.position + (Quaternion.Euler(0, angleBtwAttacker, 0) * player.forward) * fightRange;
            Vector3 pos2 = player.position + (Quaternion.Euler(0, -angleBtwAttacker, 0) * player.forward) * fightRange;

            Gizmos.color = twinAttackColour;
            //Vector3 pos1 = (Quaternion.Euler(0, angleBtwAttacker, 0) * pos0);
            //Vector3 pos2 = (Quaternion.Euler(0, -angleBtwAttacker, 0) * pos0);
            Gizmos.DrawWireSphere(pos1, 0.5f);
            Gizmos.DrawWireSphere(pos2, 0.5f);
        }

    }

}
