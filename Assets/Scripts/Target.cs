using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public Action<Collision> onCollisionEnter;
   
    public void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter(collision);
    }
}
