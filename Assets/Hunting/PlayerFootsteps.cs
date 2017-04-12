using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
	public GameObject footstepPrefab;
	public float footstepIncrement = 0.5f;

	private float timer = 0f;

	private TPCharController plController;


	void Start ()
	{
		plController = GameObject.FindWithTag("Player").GetComponent<TPCharController>();
	}

	void Update()
	{
		if (plController.IsMoving ())
			timer += Time.deltaTime;

		if (timer >= footstepIncrement)
		{
			timer -= footstepIncrement;
			DoFootstep ();
		}
	}


	private void DoFootstep()
	{
		if (!footstepPrefab)
			return;

		GameObject g = (GameObject)Instantiate (footstepPrefab, transform.position, Quaternion.identity);
		Destroy (g, 0.5f);
	}


}
