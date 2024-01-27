using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkData : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject holder;
    [SerializeField] Drinks drink;
    [SerializeField] float ScaleFactor = 0.8f;

    private void Start()
    {
        Instantiate(drink.DrinkObject, gameObject.transform);
    }

    public void Consume()
    {
        // Consume logic
    }

    public void Pickup()
    {
        Debug.Log("Pickup method called on " + gameObject.name);
        GameObject spawnedObj = Instantiate(drink.DrinkObject, holder.transform);
        spawnedObj.transform.localScale -= new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
    }
}

