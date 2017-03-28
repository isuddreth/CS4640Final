using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myarrow : MonoBehaviour 
{
	public float speed = 10;
	public float flyTime;
	 

	private bool flying = true;
	private float stopTime;
	private Transform anchor;

	void Start ()
	{
		this.stopTime = Time.time + this.flyTime;
	}

	void Update ()
	{
		this.transform.Translate (Vector3.forward * speed * Time.deltaTime);

		/*if (this.stopTime <= Time.time && this.flying) {
			GameObject.Destroy (gameObject);
		}*/



	}

	void OnCollisionEnter (Collision other)
	{
		transform.parent = other.gameObject.transform;

		}

	}

