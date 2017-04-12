using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeerHit : MonoBehaviour {
	public float timer = 30f;
	public int newTarget;
	public Vector3 target;
	public GameObject deer;
	public GameObject drop = null;
	public bool hit = false;
	public float lifetime = 2f;
	public FoodManager food;
	public FoodDrop dropping;

	private NavMeshAgent navAgent;
	public AnimalBehavior animal;

	void Start () {
		navAgent = GetComponent<NavMeshAgent> ();
		animal = GetComponent<AnimalBehavior> ();

	}

	void Update () 
	{

		if (hit) {
			Destroy (deer, lifetime);

		} else {
			Destroy (deer, timer);
		}



	}



	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Arrow") {

			deer.transform.Rotate (0, 0, -90);
			navAgent.Stop ();
			animal.enabled = false;
			Instantiate (drop, transform.position, transform.rotation);
			drop.tag = "Food";

			hit = true;

		}


		else 
		{
			return;
		}

	}


}

