using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenTable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// GUI FUNCTION
	void OnGUI()
	{
		//Access the 'InReach' variable from raycasting script.
		GameObject Player = GameObject.Find("Player");
		Detection detection = Player.GetComponent<Detection>();

		if (detection.castleFoodInReach == true)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(20, 40, 200, 25), "Kitchen Table");
		}
	}
}
