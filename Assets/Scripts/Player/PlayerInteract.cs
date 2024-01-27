using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] Drinks Currentdrink;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithObjectInFront();
        }
    }

    private void InteractWithObjectInFront()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();
            if (interactableObject != null)
            {
                
                interactableObject.Pickup();
               
                interactableObject.Destroy();
            }
        }
    }
}
