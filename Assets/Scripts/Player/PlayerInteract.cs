using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] Drinks Currentdrink;
    [SerializeField] PlayerStats CurrentplayerStats;
    [SerializeField] Animator animator;
    [SerializeField] GameObject firepoint;
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
                    interactableObject.Destroy();
                }
            }
        }
    }

    private void ApplyEffect()
    {
        StatusEffects currentStatus = Currentdrink.statusEffects;
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
                GameObject drinkInstance = Instantiate(Currentdrink.DrinkObject, firepoint.transform.position, firepoint.transform.rotation);
                drinkInstance.SetActive(true);

                // Set the scale if necessary
                drinkInstance.transform.localScale = new Vector3(5f, 5f, 5f);

                // Add the collider and rigidbody
                drinkInstance.AddComponent<BoxCollider>();
                Rigidbody rb = drinkInstance.AddComponent<Rigidbody>();
                rb.isKinematic = false;

                // Use the firepoint's forward direction for the force and neutralize the y component to avoid shooting downwards
                Vector3 forceDirection = firepoint.transform.forward;

                // Adjust the force magnitude to control the speed of the projectile
                // You might need to tweak this value to get the desired speed
                float forceMagnitude = CurrentplayerStats.attackPower;

                // Apply the force to the Rigidbody to propel it forward
                rb.AddForce(forceMagnitude * forceDirection, ForceMode.Impulse);

                // Clear the current drink
                Currentdrink = null;
                break;
        default : break;
        

        }
        
        
    }
}
