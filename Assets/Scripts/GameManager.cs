using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
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
   
    /// <summary>
    /// Standard monobehaviour initializer.
    /// </summary>
    void Start () {
        Instance = this;
        _cannonRotationOriginal = cannon.transform.rotation;
    }
	
	/// <summary>
	/// Called once per frame.
	/// </summary>
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

    /// <summary>
    /// Increases the fire power of the cannon
    /// by the value defined in "firePowerIncrementer".
    /// </summary>
    public void increaseFirePower()
    {
        powerCharge += firePowerIncrementer;
        if (powerCharge > firePowerMax)
        {
            powerCharge = firePowerMax;
        }
    }

    /// <summary>
    /// Resets the cannon transform, projectile transform, and firing values.
    /// </summary>
    public void resetLaunchpad()
    {
        // reset flyer to the cannon location and make transparent
        // TODO: rotate so flyer flies towards head
        // flyer.GetComponent<Flyer>().resetFlyer( cannon.transform.position, cannon.transform.rotation );
        
        // firepower reset
        resetPower();

        // cannon reset
        cannon.GetComponent<Cannon>().resetFirepowerIndicator();
    }

    /// <summary>
    /// Resets the power charge value.
    /// </summary>
    public void resetPower()
    {
        powerCharge = firePowerReset;
    }

    /// <summary>
    /// Rotates the cannon's pitch.
    /// </summary>
    /// <param name="pPitch">New pitch rotation value.</param>
    public void setCannonPitch(float pPitch)
    {
        cannonPitch = Mathf.Max(Mathf.Min(cannonPitchMax, pPitch), cannonPitchMin);
    }

    /// <summary>
    /// Fires the projectile (Flyer).
    /// TODO: add particles
    /// </summary>
    public void fire()
    {
        // Debug.Log("fire");
        // rotate the flyer -90 so it flies head first
        // Vector3 tFireVector = new Vector3(_cannonPitch - 90f, 0, 0) * _powerCharge;

        Quaternion tAdjustedRotation = cannon.transform.localRotation;
        tAdjustedRotation = tAdjustedRotation * Quaternion.Euler(0, 180, 0);
        tAdjustedRotation = tAdjustedRotation * Quaternion.Euler(90, 0, 0);
        flyer.GetComponent<Flyer>().resetFlyer(cannon.transform.position, tAdjustedRotation );

        // Vector3 tFireVector = cannon.transform.rotation.ToEuler();

        float tFirePower = powerCharge * firePowerMultiplier;
        flyer.GetComponent<Flyer>().fireProjectile(tFirePower);
    }
}
