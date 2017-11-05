using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameObject cannon;
    public GameObject flyer;
    public GameObject target;
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
        GameManager.Instance = this;
        _cannonRotationOriginal = cannon.transform.rotation;

        target.GetComponent<Target>().onCollisionEnter += targetCollision;
    }
	
	/// <summary>
	/// Called once per frame.
	/// </summary>
	void Update () {
        #if UNITY_EDITOR
            _cannonPitch += Input.GetAxis("Vertical");
            setCannonPitch(_cannonPitch);
        #endif

        cannon.transform.rotation = Quaternion.Euler(cannonPitch, 180f, 0);

        if (Input.GetKey("space") || ControllerManager.Instance.triggerHeld)
        {
            increaseFirePower();
            playGameObjectSound(cannon, "Charging");

            // set cannon firepower indicator by power
            cannon.GetComponent<Cannon>().setFirepowerIndicatorPositionByPower( powerCharge );
        }

        // Fire
        if (Input.GetKeyUp("space"))
        {
            fire();
            playGameObjectSound(cannon, "Explosion");
            playGameObjectSound(flyer, "Scream");
            stopGameObjectSound(cannon, "Charging");
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
        // hide flyer
        flyer.SetActive(false);
        
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
        Quaternion tAdjustedRotation = cannon.transform.localRotation;
        tAdjustedRotation = tAdjustedRotation * Quaternion.Euler(0, 180, 0);
        tAdjustedRotation = tAdjustedRotation * Quaternion.Euler(90, 0, 0);
        flyer.GetComponent<Flyer>().resetFlyer(cannon.transform.position, tAdjustedRotation );

        float tFirePower = powerCharge * firePowerMultiplier;
        flyer.GetComponent<Flyer>().fireProjectile(tFirePower);
    }

    public void playGameObjectSound(GameObject gameObj, string soundName)
    {
        AudioSource[] sounds = gameObj.transform.GetComponentsInChildren<AudioSource>();
        foreach(AudioSource sound in sounds)
        {
            if (sound.name == soundName)
            {
                sound.Play();
            }
        }
    }

    public void stopGameObjectSound(GameObject gameObj, string soundName)
    {
        AudioSource[] sounds = gameObj.transform.GetComponentsInChildren<AudioSource>();
        foreach(AudioSource sound in sounds)
        {
            if (sound.name == soundName)
            {
                sound.Stop();
            }
        }
    }
    
    public void targetCollision( Collision pCollision )
    {
        Debug.LogFormat("target collision: {0}", pCollision.other.name );
        if( pCollision.other.tag == "Player")
        {
            Debug.Log("Success!");
            playGameObjectSound(target, "Success");
        }
    }
}
