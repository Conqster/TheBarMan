using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkData : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject holder;
    public Drinks drink;
    [SerializeField] float ScaleFactor = 0.9f;

    private void Start()
    {
       //GameObject SpawnedDrink =  Instantiate(drink.DrinkObject, gameObject.transform);
        //SpawnedDrink.transform.localScale += new Vector3(ScaleFactor-0.4f, ScaleFactor - 0.4f, ScaleFactor - 0.4f);
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

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

