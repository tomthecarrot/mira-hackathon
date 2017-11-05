using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "Flyer" projectile class.
/// Mira Prism Hackathon 2017
/// </summary>
public class Flyer : MonoBehaviour {

    /// <summary>
    /// The rigidbody attached to the projectile.
    /// </summary>
    private Rigidbody _rb;

    /// <summary>
    /// The original rotation of the projectile,
    /// before it is fired.
    /// </summary>
    private Quaternion _originalRotation;

    /// <summary>
    /// The original position of the projectile,
    /// before it is fired.
    /// </summary>
    private Vector3 _originalPosition;

    /// <summary>
    /// Standard monobehaviour initalizer.
    /// </summary>
    void Start ()
    {
        _rb = GetComponent<Rigidbody>();
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }
	
    /// <summary>
    /// Called once per frame.
    /// </summary>
	void Update ()
    {
        // empty
    }

    /// <summary>
    /// Applies force to fire the projetile.
    /// </summary>
    /// <param name="pFireVector">The vector describing the direction and intensity of the fire force.</param>
    public void fireProjectile( Vector3 pFireVector )
    {
        Debug.LogFormat( "Fire!, {0}, {1}, {2}", pFireVector.x, pFireVector.y, pFireVector.z );

        // Enable the projectile
        gameObject.SetActive(true);
        
        // Apply the force
        _rb.AddForce( pFireVector, ForceMode.Impulse );
    }

    /// <summary>
    /// Resets the position and rotation of the projectile
    /// to the given transform.
    /// </summary>
    /// <param name="pTransform">New transform for the projectile gameobject.</param>
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
