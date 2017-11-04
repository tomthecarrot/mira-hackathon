using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour {

    private Rigidbody _rb;
    private Quaternion _originalRotation;
    private Vector3 _originalPosition;
    public float powerCharge = 1;

    public float firePowerMultiplier = 1;
    public float firePowerIncrementer = 0.001f;
    public float firePowerReset = 1;
    public float firePowerMax = 3;

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody>();
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {

        // FirePower
        if (Input.GetKey("space"))
        {
            resetFlyer();

            powerCharge += firePowerIncrementer;
            if( powerCharge > firePowerMax )
            {
                powerCharge = firePowerMax;
            }
        }
        
        // Fire
        if (Input.GetKeyUp("space"))
        {
            fireProjectile();
        }

        // Orientation
      
    }

    public void fireProjectile()
    {
        Debug.Log("Fire!");
        _rb.AddForce(transform.up * firePowerMultiplier * powerCharge, ForceMode.Impulse);
        resetPower();
    }

    public void resetPower()
    {
        powerCharge = firePowerReset;
    }

    public void resetFlyer()
    {
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;
    }

}
