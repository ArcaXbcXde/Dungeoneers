using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Mechanicals {
	
	[Min (0.0f)]
	public float deactivationTime = 1.0f;

	protected int countObjectsPressing = 0;

	protected void Update () {
		if ((countObjectsPressing <= 0) && (isPressed == true)) {

			isPressed = false;
			Invoke("Deactivate", deactivationTime);
		}
	}

	public override void Activate () {

		spriteRenderer.sprite = onStateSprite;
		for (int i = 0; i < objectToActivate.Length; i++) {

			objectToActivate[i].Activate();
		}
	}

	public override void Deactivate () {

		spriteRenderer.sprite = offStateSprite;
		for (int i = 0; i < objectToActivate.Length; i++) {

			objectToActivate[i].Deactivate();
		}
	}

	protected override void OnTriggerEnter2D (Collider2D col) {

		if (col.tag != "Ground") {

			countObjectsPressing ++;
			if (isPressed == false) {

				isPressed = true;
				Activate();
			}
		}
	}

	protected override void OnTriggerStay2D (Collider2D col) { }

	protected override void OnTriggerExit2D (Collider2D col) {
		
		if (col.tag != "Ground") {

			countObjectsPressing --;
		}
	}

}
