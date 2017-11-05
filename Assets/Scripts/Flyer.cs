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

    public void fireProjectile( Vector3 pFireVector )
    {
        Debug.LogFormat( "Fire!, {0}, {1}, {2}", pFireVector.x, pFireVector.y, pFireVector.z );

        gameObject.SetActive(true);
        
        _rb.AddForce( pFireVector, ForceMode.Impulse );
    }

    public void resetFlyer( Transform pTransform )
    {
        // position reset
        // transform.position = _originalPosition;
        // transform.rotation = _originalRotation;

        transform.position = pTransform.position;
        transform.rotation = pTransform.rotation;

        // gameObject.SetActive(false);
    }
}
