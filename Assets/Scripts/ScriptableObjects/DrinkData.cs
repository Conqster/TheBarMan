using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkData : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject holder;
    public Drinks drink;
    [SerializeField] float ScaleFactor = 0.9f;
    private GameObject currObj;

    private void Start()
    {
        //GameObject SpawnedDrink =  Instantiate(drink.DrinkObject, gameObject.transform);
        //SpawnedDrink.transform.localScale += new Vector3(ScaleFactor-0.4f, ScaleFactor - 0.4f, ScaleFactor - 0.4f);
        holder = GameObject.FindWithTag("Holder");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            other.gameObject.GetComponent<BarAI>().GetHealthSystem().DealDamage(10);
            Destroy(gameObject);
        }
    }
    public void Consume()
    {
        Destroy(currObj);
        GameObject SpawnedObj = Instantiate(drink.EmptyDrink, holder.transform);
        SpawnedObj.transform.localScale -= new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
        currObj = SpawnedObj;
    }

    public void Pickup()
    {
        Debug.Log("Pickup method called on " + gameObject.name);
        GameObject spawnedObj = Instantiate(drink.DrinkObject, holder.transform);
        spawnedObj.transform.position = spawnedObj.transform.parent.position;
       //spawnedObj.transform.localScale -= new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
        currObj = spawnedObj;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

