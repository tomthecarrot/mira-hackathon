using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject cannon;
    public GameObject flyer;
    public float firePowerReset = 0.5f;
    public float firePowerMax = 3;
    public float firePowerMultiplier = 1;
    public float firePowerIncrementer = 0.001f;
    public float cannonPitch;
    public float cannonPitchMin = 0;
    public float cannonPitchMax = 180;
    public float powerCharge = 1;

    private Quaternion _cannonRotationOriginal;
    private float _cannonPitch = 0;
   
    void Start () {
        _cannonRotationOriginal = cannon.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        _cannonPitch += Input.GetAxis("Vertical");
        setCannonPitch(_cannonPitch);

        cannon.transform.rotation = Quaternion.Euler(cannonPitch, 180f, 0);

        // FirePower
        if (Input.GetKeyDown("space"))
        {
           resetLaunchpad();
        }

        if (Input.GetKey("space"))
        {
            increaseFirePower();

            // set cannon firepower indicator by power
            cannon.GetComponent<Cannon>().setFirepowerIndicatorPositionByPower( powerCharge );
        }

        // Fire
        if (Input.GetKeyUp("space"))
        {
            fire();
        }
    }

    public void increaseFirePower()
    {
        powerCharge += firePowerIncrementer;
        if (powerCharge > firePowerMax)
        {
            powerCharge = firePowerMax;
        }
    }

    public void resetLaunchpad()
    {
        // reset flyer to the cannon location and make transparent
        flyer.GetComponent<Flyer>().resetFlyer( cannon.transform );
        
        // firepower reset
        resetPower();

        // cannon reset
        cannon.GetComponent<Cannon>().resetFirepowerIndicator();
    }

    public void resetPower()
    {
        powerCharge = firePowerReset;
    }

    // Cannon
    public void setCannonPitch(float pPitch)
    {
        cannonPitch = Mathf.Max(Mathf.Min(cannonPitchMax, pPitch), cannonPitchMin);
    }

    // Fire -- fire flyer, particles, etc.
    public void fire()
    {
        Debug.Log("fire");
        // rotate the flyer -90 so it flies head first
        // Vector3 tFireVector = new Vector3(_cannonPitch - 90f, 0, 0) * _powerCharge;
        Vector3 tFireVector = cannon.transform.rotation.ToEuler() * powerCharge * firePowerMultiplier;

        flyer.GetComponent<Flyer>().fireProjectile(tFireVector);
    }
}
