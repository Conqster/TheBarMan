using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Perception
{
    public static List<Rigidbody> Vision(Rigidbody user, float visionLength, string target = "Character")
    {

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        RaycastHit[] hits = Physics.SphereCastAll(user.position, visionLength, -Vector3.up);

        foreach (RaycastHit hit in hits)
            if(hit.rigidbody !=  null && hit.collider.CompareTag("Character") && hit.rigidbody != user)
                rigidbodies.Add(hit.rigidbody);

        return rigidbodies;
    }


}
