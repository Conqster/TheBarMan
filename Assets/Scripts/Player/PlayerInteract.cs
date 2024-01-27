using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] Drinks Currentdrink;
    [SerializeField] PlayerStats CurrentplayerStats;
    [SerializeField] Animator animator;
    [SerializeField] IInteractable interObj;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithObjectInFront();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ApplyEffect();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("swing") && stateInfo.normalizedTime >= 0.95f)
        {
            animator.SetBool("IsAttacking", false);
        }
        if (stateInfo.IsName("Drink") && stateInfo.normalizedTime >= 0.95f)
        {
            animator.SetBool("IsDrinking", false);
        }
    }
    private void InteractWithObjectInFront()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Currentdrink == null)
            {
                IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();
                if (interactableObject != null)
                {

                    interactableObject.Pickup();
                    Currentdrink = hit.collider.GetComponent<DrinkData>().drink;
                    interObj = interactableObject;
                    interactableObject.Destroy();
                }
            }
        }
    }

    private void ApplyEffect()
    {
        animator.SetBool("IsDrinking", true);
        StatusEffects currentStatus = Currentdrink.statusEffects;
        interObj.Consume();
      
        print(Currentdrink.statusEffects);
         switch(currentStatus)
        {

            case StatusEffects.damage:
                CurrentplayerStats.ModifyAttackPower(10);
                CurrentplayerStats.ModifySobrietyLevel(10);
                break;
            case StatusEffects.health:
                CurrentplayerStats.ModifyHealth(10);
                CurrentplayerStats.ModifySobrietyLevel(10);
                break;
            case StatusEffects.damageReduction:
                CurrentplayerStats.ModifyDefense(10);
                CurrentplayerStats.ModifySobrietyLevel(10);
                break;
            case StatusEffects.speed:
                CurrentplayerStats.ModifySpeed(10);
                CurrentplayerStats.ModifySobrietyLevel(10);
                break;
            case StatusEffects.random:
                CurrentplayerStats.RandomBuff(Random.Range(-10,10));
                CurrentplayerStats.ModifySobrietyLevel(20);
                break;
            case StatusEffects.jungleJuice:
                CurrentplayerStats.JungleJuice(15);
                break;
            default: break;
        }
    }


    public void Attack()
    {
        Drinks attackDrink = Currentdrink;
        switch(attackDrink.fireType) {

            case FireType.Meele:
                animator.SetBool("IsAttacking", true);
                break;
            case FireType.Projectile:
                break;
            case FireType.throwable:
                break;
        default : break;
        

        }
        
        
    }
}
