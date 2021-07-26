using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivationTimer : MonoBehaviour {

	public float disableTime = 3.0f;

	private void OnEnable () {
		
		Invoke ("Disable", disableTime);
	}

	private void Disable () {

		gameObject.SetActive(false);
	}
}
