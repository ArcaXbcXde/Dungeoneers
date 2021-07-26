using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleEnableButtonTrigger : MonoBehaviour {

	public Button buttonToEnable;

	private void OnEnable () {
		
		buttonToEnable.interactable = true;
	}
}
