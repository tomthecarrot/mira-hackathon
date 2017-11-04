using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour {

    private Rigidbody _rb;
    private Quaternion _originalRotation;
    private Vector3 _originalPosition;




    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody>();
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }
	
	void Update () {
        // empty
    }

    public void fireProjectile( Vector3 pFireVector )
    {
        Debug.Log("Fire!");
        _rb.AddForce( pFireVector, ForceMode.Impulse );
    }

    public void resetFlyerTransform()
    {
        // position reset
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;
    }

}
