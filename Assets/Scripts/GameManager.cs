using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

/// <summary>
/// Game logic manager.
/// Mira Prism Hackathon 2017
/// </summary>
public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameObject cannon;
    public GameObject flyer;
    public GameObject target;
    public GameObject firePowerIndicator;
    public GameObject targetParticles;
    public float firePowerReset = 0.5f;
    public float firePowerMax = 3;
    public float firePowerMultiplier = 1;
    public float firePowerIncrementer = 0.001f;
    public float cannonPitch;
    public float cannonPitchMin = 0;
    public float cannonPitchMax = 180;
    public float muzzleOffset;
    public float powerCharge = 1;
    public Vector3 gravityOverride;

    private Quaternion _cannonRotationOriginal;
    private float _cannonPitch = 0;
   
    /// <summary>
    /// Standard monobehaviour initializer.
    /// </summary>
    void Start () {
        GameManager.Instance = this;
        _cannonRotationOriginal = cannon.transform.rotation;

        target.GetComponent<Target>().onCollisionEnter += targetCollision;

        firePowerIndicator.SetActive(false);

        Physics.gravity = gravityOverride;
    }
	
	/// <summary>
	/// Called once per frame.
	/// </summary>
	void Update () {
        if (PhotonNetwork.isMasterClient) {
            #if UNITY_EDITOR
                _cannonPitch += Input.GetAxis("Vertical");
                setCannonPitch(_cannonPitch);
            #endif

            cannon.transform.rotation = Quaternion.Euler(cannonPitch, 180f, 0);
        }

        if(Input.GetKeyDown("space"))
        {
            GameManager.Instance.resetLaunchpad();
            playGameObjectSound(cannon, "Charging");
        }

        if (Input.GetKey("space") || ControllerManager.Instance.triggerHeld)
        {
            increaseFirePower();
            

            // set cannon firepower indicator by power
            cannon.GetComponent<Cannon>().setFirepowerIndicatorPositionByPower( powerCharge );
        }

        // Fire
        if (Input.GetKeyUp("space"))
        {
            fire();
            playFireSounds();
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

        // show fire indicator
        firePowerIndicator.SetActive(true);

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
    /// </summary>
    public void fire()
    {
        Quaternion tAdjustedRotation = cannon.transform.localRotation;
        tAdjustedRotation = tAdjustedRotation * Quaternion.Euler(90, 180, 0);
        flyer.GetComponent<Flyer>().resetFlyer(cannon.transform.position, tAdjustedRotation );

        float tFirePower = powerCharge * firePowerMultiplier;
        flyer.GetComponent<Flyer>().fireProjectile(tFirePower);

        // hide fire indicator
        firePowerIndicator.SetActive( false );
    }

    /// <summary>
    /// Plays the fusing/charging sound
    /// </summary>
    public void playFuseSound()
    {
        playGameObjectSound(cannon, "Charging");
    }

    /// <summary>
    /// Plays each fire sound
    /// </summary>
    public void playFireSounds()
    {
        playGameObjectSound(cannon, "Explosion");
        playGameObjectSound(flyer, "Scream");
        stopGameObjectSound(cannon, "Charging");
    }

    /// <summary>
    /// Plays the sound associated with a game object.
    /// </summary>
    /// <param name="gameObj">The game object from which to get the sound.</param>
    /// <param name="soundName">The name of the sound to play.</param>
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

    /// <summary>
    /// Stops playing the sound associated with a game object.
    /// </summary>
    /// <param name="gameObj">The game object from which the sound is playing.</param>
    /// <param name="soundName">The name of the sound to stop.</param>
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

            targetParticles.transform.position = pCollision.collider.transform.position;
            targetParticles.GetComponent<ParticleSystem>().Play();
        }
    }
}
