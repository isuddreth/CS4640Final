using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHurt : MonoBehaviour {
	public PlayerHealth health;
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

		

	void OnTriggerEnter(Collider other)
		{
		if (other.gameObject.tag == "Player")
			{
			health.TakeDamage (100f);
			}
			
		
	}
}
