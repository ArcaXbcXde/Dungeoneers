using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPositions : MonoBehaviour {

	public GameObject objectToFollow;
	
	public void SetThisPosition (GameObject obj) {
		objectToFollow = obj;
		transform.position = objectToFollow.transform.position;
	}
}
