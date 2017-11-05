using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour {

    private Rigidbody _rb;
    private Quaternion _originalRotation;
    private Vector3 _originalPosition;

    void Start ()
    {
        _rb = GetComponent<Rigidbody>();
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }
	
	void Update ()
    {
        // empty
    }

    // rotate it first
    public void fireProjectile( float pFirePower )
    {
        Debug.LogFormat( "Fire! power:, {0}", pFirePower );

        gameObject.SetActive(true);

        _rb.AddForce( transform.up * pFirePower, ForceMode.Impulse);
    }

    public void resetFlyer( Vector3 pPosition, Quaternion pRotation )
    {
        // position reset
        // transform.position = _originalPosition;
        // transform.rotation = _originalRotation;

        transform.position = pPosition;
        transform.localRotation = pRotation;

        // gameObject.SetActive(false);
    }
}
