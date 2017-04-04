using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerHit : MonoBehaviour {

	public GameObject deer;
	public bool hit = false;
	public float lifetime = 5f;
	// Use this for initialization
	void Start () {
		//transform.localEulerAngles = new Vector3(360,-180,90);
	}

	// Update is called once per frame
	void Update () {
		if (hit)

		{
			Destroy (deer, lifetime);
		}
	}


	void OnTriggerEnter (Collider other)
	{
		deer.transform.Rotate(0, 0, -90);
		hit = true;

	}


}
