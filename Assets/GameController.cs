using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject cannon;
    public GameObject flyer;
    public float firePowerReset = 1;
    public float firePowerMax = 3;
    public float firePowerMultiplier = 1;
    public float firePowerIncrementer = 0.001f;
    public float cannonPitch;
    public float cannonPitchMin = 0;
    public float cannonPitchMax = 180;

    private Quaternion _cannonRotationOriginal;
    private float _powerCharge = 1;
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
        if (Input.GetKeyDown("z"))
        {
            resetLaunchpad();
        }

        if (Input.GetKey("z"))
        {
            increaseFirePower();

            // set cannon firepower indicator by power
            cannon.GetComponent<Cannon>().setFirepowerIndicatorPositionByPower( _powerCharge );
        }

        // Fire
        if (Input.GetKeyUp("z"))
        {
            // rotate the flyer -90 so it flies head first
            Vector3 tFireVector = new Vector3( _cannonPitch - 90f, 0, 0 ) * _powerCharge;

            flyer.GetComponent<Flyer>().fireProjectile( tFireVector );
        }
    }

    public void increaseFirePower()
    {
        _powerCharge += firePowerIncrementer;
        if (_powerCharge > firePowerMax)
        {
            _powerCharge = firePowerMax;
        }
    }

    public void resetLaunchpad()
    {
        flyer.GetComponent<Flyer>().resetFlyerTransform();
        
        // firepower reset
        resetPower();

        // cannon reset
        cannon.GetComponent<Cannon>().resetFirepowerIndicator();
    }

    public void resetPower()
    {
        _powerCharge = firePowerReset;
    }

    // Cannon
    public void setCannonPitch(float pPitch)
    {
        cannonPitch = Mathf.Max(Mathf.Min(cannonPitchMax, pPitch), cannonPitchMin);
    }
}
