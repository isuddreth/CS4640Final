using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mybow : MonoBehaviour {

	public int theRange = 1000;
	public LineRenderer theLaser = null;
	public GameObject arrow = null;
	
	private Vector3 thePosition = Vector3.zero;
	private Vector3 theDirection = Vector3.zero;
	private Vector3 theEndPoint = Vector3.zero;
	private RaycastHit hit;
	private bool isOut = false;
	
	void Start () 
	{
		theLaser = this.gameObject.GetComponent<LineRenderer> ();
	}
	
	void FixedUpdate () 
	{
		thePosition = this.transform.position;
		theDirection = this.transform.TransformDirection(Vector3.forward);
		
		theLaser.SetPosition(0,thePosition);
		
		if(Physics.Raycast (thePosition, theDirection, out hit, theRange))
		{
				theLaser.SetPosition(1,hit.point);
			theEndPoint = thePosition + theDirection * theRange;
			theLaser.SetPosition (1, theEndPoint);
		}
		
	}


	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			isOut = true;

		}

		if (isOut) 
		{
			GameObject cloneArrow = (GameObject)Instantiate (arrow, thePosition, Quaternion.identity);
			cloneArrow.transform.position = thePosition;
			cloneArrow.transform.rotation = this.transform.rotation;
			isOut = false;
		}
}

}
