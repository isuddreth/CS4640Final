using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalBehavior : MonoBehaviour {

	public float speed = 5;
	public float directionChangeInterval = 1;
	public float maxHeadingChange = 30;
	public NavMeshAgent agent;
	CharacterController controller;
	float heading;
	Vector3 targetRotation;
	private GameObject[] runToLocation;
	private int pickAPoint;
	private GameObject hidePoint;


	void Awake ()
	{
		controller = GetComponent<CharacterController>();
		heading = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, heading, 0);
		agent = GetComponent<NavMeshAgent> ();
		StartCoroutine(NewHeading());

		runToLocation = GameObject.FindGameObjectsWithTag ("DeerWalk");
		pickAPoint = Random.Range (0, runToLocation.Length);
		hidePoint = runToLocation[pickAPoint];
	}

	void Update ()
	{
		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
		var forward = transform.TransformDirection(Vector3.forward);
		controller.SimpleMove(forward * speed);



	}
	IEnumerator NewHeading ()
	{
		while (true) {
			NewHeadingRoutine();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}
	void NewHeadingRoutine ()
	{
		var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
		var ceil  = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
		heading = Random.Range(floor, ceil);
		targetRotation = new Vector3(0, heading, 0);
	}

	void OnTriggerEnter (Collider other){

		if (other.gameObject.tag == "Deer") {

			agent.SetDestination(hidePoint.transform.position);
		}
	}
}
