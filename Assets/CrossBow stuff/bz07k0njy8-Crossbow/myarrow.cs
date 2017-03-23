using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myarrow : MonoBehaviour 
{
	public float speed = 10;


	void Update ()
	{
		this.transform.Translate (Vector3.forward * speed * Time.deltaTime);

	}
}
