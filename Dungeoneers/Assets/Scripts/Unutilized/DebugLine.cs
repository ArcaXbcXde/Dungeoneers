using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLine : MonoBehaviour {

    public void DebugLineSay () {

		Debug.Log("Debug says hi");
	}

	public void DebugLineSay (string phrase) {

		Debug.Log (phrase);
	}
}
