using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

    public float oscillationDistance;
    public float oscillationSpeed;

    private Vector3 _originalPosition;

    public void Start()
    {
        _originalPosition = transform.position;
    }

    public void Update()
    {
        transform.position = new Vector3(_originalPosition.x, _originalPosition.y + oscillationDistance * Mathf.Sin(Time.time * oscillationSpeed), _originalPosition.z);
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
