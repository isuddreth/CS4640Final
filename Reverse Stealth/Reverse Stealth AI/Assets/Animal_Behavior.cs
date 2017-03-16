using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal_Behavior : MonoBehaviour {
	private Transform player;
	private NavMeshAgent nm;
	private float nextTurnTime;
	private Transform startTransform;
	public float multiplyBy;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nm = this.GetComponent<NavMeshAgent> ();
		RunFrom ();
	}
	
	void Update () {
		if (Time.time > nextTurnTime) {
			RunFrom ();
			}
}
	public void RunFrom()
	{
		startTransform = transform;

		transform.rotation = Quaternion.LookRotation(transform.position - player.position);
		Vector3 run = transform.position + transform.forward * multiplyBy;
		NavMeshHit hit;  
		NavMesh.SamplePosition(run, out hit, 5, 1 << NavMesh.GetNavMeshLayerFromName("Default")); 
		nextTurnTime = Time.deltaTime + 5;
		transform.position = startTransform.position;
		transform.rotation = startTransform.rotation;
		nm.SetDestination(hit.position);

	}

}
