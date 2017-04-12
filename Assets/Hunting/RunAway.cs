using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : MonoBehaviour {


	public float sightRange = 16f;
	[Range(0f, 180f)]
	public float fieldOfView = 120f;
	public float hearingRange = 16f;
	public float alertnessThreshold = 10f;
	public float visibilityThreshold = 0.25f;

	private bool foundPlayer = false;
	private float alertness = 0f;
	private float alertnessRefereshTimer = 10f;
	private float soundAlertness = 0f;

	private Vector3 lastKnownPosition;
	private Vector3 lastSoundPosition;
	private Vector3 initialPosition;

	//private GameObject runToLocation;
	private GameObject[] runToLocation;
	private int pickAPoint;
	private GameObject hidePoint;

	private GameObject player;
	private NavMeshAgent navAgent;

	void Start () {
		//runToLocation = GameObject.FindGameObjectWithTag ("GoToSpot");
		runToLocation = GameObject.FindGameObjectsWithTag ("GoToSpot");
		pickAPoint = Random.Range (0, runToLocation.Length);
		hidePoint = runToLocation[pickAPoint];

		initialPosition = transform.position;
		lastKnownPosition = Vector3.zero;
		player = GameObject.FindWithTag ("Player");
		navAgent = GetComponent<NavMeshAgent> ();
	}

	void Update () {

		foundPlayer = TestForDetection ();
		if (foundPlayer) {
			if (CanSeePlayer () || CanHearPlayer ())
				lastKnownPosition = player.transform.position;
			//navAgent.SetDestination(runToLocation.transform.position);
			navAgent.SetDestination(hidePoint.transform.position);
			//Destroy (gameObject);
		}
		else if (CanHearPlayer())
		{
			soundAlertness += Time.deltaTime;
			lastSoundPosition = GameObject.FindWithTag ("SoundObject").transform.position;
			if (soundAlertness >= 4f)
				//navAgent.SetDestination (runToLocation.transform.position);
				navAgent.SetDestination(hidePoint.transform.position);
		}
		else {
			if (Vector3.Distance (transform.position, lastSoundPosition) <= 1f)
				soundAlertness -= Mathf.Min (Time.deltaTime, soundAlertness);
			if (soundAlertness <= 0f)
			{
				lastSoundPosition = Vector3.zero;
				navAgent.SetDestination (initialPosition);

			}
		}

		if (soundAlertness > 0f && CanSeePlayer())
			foundPlayer = true;

		if (!CanSeePlayer () && !CanHearPlayer ())
			alertnessRefereshTimer -= Time.deltaTime;
		else
			alertnessRefereshTimer = 10f;
		if (alertnessRefereshTimer <= 0f)
		{
			foundPlayer = false;
			alertness = 0f;
			alertnessRefereshTimer = 10f;
			soundAlertness = 0f;
			lastKnownPosition = Vector3.zero;
			lastSoundPosition = Vector3.zero;
			navAgent.SetDestination(initialPosition);
		}
	}


	private float DistanceFromTarget (GameObject g)
	{
		if (g)
			return Vector3.Distance (transform.position, g.transform.position);
		return Mathf.Infinity;
	}

	private bool LineOfSight(GameObject g, float fov)
	{
		if (!g)
			return false;

		RaycastHit hit;
		Transform target = g.transform;

		Vector3 myPos = transform.position + (Vector3.up * 0.25f);
		Vector3 tarPos = target.position + (Vector3.up * 0.25f);

		if ((Vector3.Angle(tarPos - myPos, transform.forward) <= (fov/2f)) && Physics.Linecast(myPos, tarPos, out hit) && (hit.collider.transform == target))

		{
			return true;
		}
		return false;
	}

	private float GetPlayerVisibility()
	{
		GameObject[] lightList = GameObject.FindGameObjectsWithTag("Light");
		float vis = 0f;
		for (int i = 0; i < lightList.Length; i++)
		{
			Light l = lightList[i].GetComponent<Light>();

			float intensity = l.intensity;
			float range = l.range;
			float dist = Vector3.Distance(player.transform.position, lightList[i].transform.position);

			if (dist == 0f)
				return 1f;

			if (dist > 0f &&  dist <= range)
			{
				vis = Mathf.Max(vis, intensity * (range / dist));

			}
		}
		return vis;
	}

	private bool CanSeePlayer()
	{
		bool distTest = false;
		bool losTest = false;
		bool visTest = false;

		distTest = (DistanceFromTarget (player) <= sightRange);
		losTest = LineOfSight (player, fieldOfView);
		visTest = (GetPlayerVisibility () >= visibilityThreshold);

		return (distTest && losTest && visTest);
	}

	private bool CanHearPlayer()
	{

		GameObject soundObj = GameObject.FindWithTag ("SoundObject");

		bool soundTest = (soundObj != null);
		bool distTest = (DistanceFromTarget (soundObj) <= hearingRange);
		bool lostTest = LineOfSight (soundObj, 360f);

		if (!lostTest)
			distTest = (DistanceFromTarget (soundObj) <= hearingRange / 2f);

		if (soundObj && distTest)
			Debug.Log (soundObj.transform.position);
		return (soundTest && distTest);
	}

	private bool TestForDetection()
	{
		float distanceFromPlayer = DistanceFromTarget (player);

		if (CanSeePlayer ()) {

			float sightDistRatio = (distanceFromPlayer / sightRange);

			if (sightDistRatio >= 0.5f)
				alertness += (Time.deltaTime * ((5f * (1f - sightDistRatio))) * GetPlayerVisibility ());
			else
				alertness = alertnessThreshold;


		}
		return (alertness > alertnessThreshold);
	}
	public bool HasFoundPlayer()
	{
		return foundPlayer;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Arrow") {
			navAgent.Stop ();
		} else {
			return;
		}
	}


}


