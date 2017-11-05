using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionEnter(Collision collision)
	{
			if (collision.collider.tag == "Player" && collision.relativeVelocity != Vector3.zero)
			{
				AudioSource thudSound = GetComponent<AudioSource>();
				thudSound.Play();
			}
	}
}
