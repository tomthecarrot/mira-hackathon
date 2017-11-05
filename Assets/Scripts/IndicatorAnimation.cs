using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorAnimation : MonoBehaviour
{
	void Update () {
        transform.localRotation = transform.localRotation * Quaternion.Euler( 0, -3f, 0 );
	}
}
