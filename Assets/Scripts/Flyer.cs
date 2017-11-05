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
    /// The original transform of the projectile,
    /// before it is fired.
    /// </summary>
    private Transform _originalTransform;

    /// <summary>
    /// The original position of the projectile,
    /// before it is fired.
    /// </summary>
    private Vector3 _originalPosition;

    /// <summary>
    /// The original rotation of the projectile,
    /// before it is fired.
    /// </summary>
    private Quaternion _originalRotation;

    public float torque;

    /// <summary>
    /// Standard monobehaviour initalizer.
    /// </summary>
    void Start ()
    {
        _rb = GetComponent<Rigidbody>();
        _originalTransform = transform;
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;

        // Disable flyer gravity if in specatator mode
        if (!PhotonNetwork.isMasterClient) {
            GetComponent<Rigidbody>().useGravity = false;
        }
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
    /// <param name="pFireVector">The scalar (float) of the fire force.</param>
    public void fireProjectile( float pFirePower )
    {
        Debug.LogFormat( "Fire! power:, {0}", pFirePower );

        // Enable the projectile
        gameObject.SetActive(true);
        
        // Add force and torque to the flyer's rigidbody
        _rb.AddForce( transform.up * pFirePower, ForceMode.Impulse);
        _rb.AddRelativeTorque( new Vector3( Random.Range( -torque, torque ), Random.Range(-torque, torque ), Random.Range(-torque, torque ) ), ForceMode.Impulse );
    }

    /// <summary>
    /// Resets the position and rotation of the projectile
    /// to the original transform.
    /// </summary>
    public void resetFlyer() {
        resetFlyer(_originalPosition, _originalRotation);
    }

    /// <summary>
    /// Resets the position and rotation of the projectile
    /// to the given transform.
    /// </summary>
    /// <param name="pTransform">New transform for the projectile gameobject.</param>
    public void resetFlyer( Vector3 pPosition, Quaternion pRotation )
    {
        // Set the new object transform
        transform.position = pPosition;
        transform.localRotation = pRotation;

        // Set the rigidbody's velocity
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
