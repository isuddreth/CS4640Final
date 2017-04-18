using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoodDrop : MonoBehaviour {

	public FoodManager food;



	void Start () {

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        
    }

	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player"){
            if (food.AddPlayerFood(5f))
            {
                Destroy(this.gameObject);
            }
            
			


		}

	}
}

