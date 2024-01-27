using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10.0f)] private float visionLength = 2.0f;




    public Rigidbody Vision()
    {
        return new Rigidbody();
    }
}
