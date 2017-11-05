using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    public GameObject cannonBody;    
    public GameObject firepowerIndicator;
    public float firepowerZDistanceMultiplier = 0.1f; // positive number

    private Vector3 _firepowerIndicatorOriginalLocalPosition;
    private float _firepowerIndicatorZ;

    void Start ()
    {
        _firepowerIndicatorOriginalLocalPosition = firepowerIndicator.transform.localPosition;
     }

    public void resetFirepowerIndicator()
    {
        firepowerIndicator.transform.localPosition = _firepowerIndicatorOriginalLocalPosition;
    }

    // this class is responsible for scaling the firepower distance offset based on firepower
    public void setFirepowerIndicatorPositionByPower( float pFirepower )
    {
        // negative z fires forward
        firepowerIndicator.transform.localPosition = _firepowerIndicatorOriginalLocalPosition + new Vector3( 0, 0, pFirepower * -firepowerZDistanceMultiplier );
    }
}
