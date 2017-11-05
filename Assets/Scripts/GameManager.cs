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
    public float muzzleOffset;
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
        // hide flyer
        flyer.SetActive(false);
        
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
        Quaternion tAdjustedRotation = cannon.transform.localRotation;
        tAdjustedRotation = tAdjustedRotation * Quaternion.Euler(0, 180, 0);
        tAdjustedRotation = tAdjustedRotation * Quaternion.Euler(90, 0, 0);
        flyer.GetComponent<Flyer>().resetFlyer(cannon.transform.position, tAdjustedRotation );

        float tFirePower = powerCharge * firePowerMultiplier;
        flyer.GetComponent<Flyer>().fireProjectile(tFirePower);
    }
}
