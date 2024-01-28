using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slot
{
    public int id;
    public Vector3 position;
}

public class SlotSystemAttack
{
    public Dictionary<Slot, bool> slots = new Dictionary<Slot, bool>(); 



    bool RequireASlot(ref Slot slot)
    {
        foreach(var s in slots)
            if(!s.Value)
            {
                slot = s.Key;
                return true;
            }

        return false;
    }

    public void UpdateSlot()
    {

    }
}
