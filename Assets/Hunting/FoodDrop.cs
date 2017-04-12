using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoodDrop : MonoBehaviour {

	public FoodManager food;
	public PlayerHealth health;
	public GameObject dropFood;
	public DeerHit deerHit;



	void Start () {
	}

	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player"){
			food.AddPlayerFood (10f);
			Destroy (this.gameObject);


		}

	}
}

