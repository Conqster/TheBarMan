using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Create Drink", menuName ="Drink")]
public class Drinks : ScriptableObject
{
    public GameObject DrinkObject;
    public GameObject EmptyDrink;
   public string drinkName;
    public StatusEffects statusEffects;
    public FireType fireType;
}
