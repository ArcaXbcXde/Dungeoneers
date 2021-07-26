using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSwitch : Switch {

	public float timeToUnpress;

	public override void Activate () {

		base.Activate();
		Invoke("Deactivate", timeToUnpress);
	}
}