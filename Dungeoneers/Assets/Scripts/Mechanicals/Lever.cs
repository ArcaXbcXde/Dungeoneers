using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Mechanicals {

	private bool isColliding = false;

	public override void Activate () {

		isPressed = true;
		spriteRenderer.flipX = true;
		for (int i = 0; i < objectToActivate.Length; i++) {

			objectToActivate[i].Activate();
		}
	}

	public override void Deactivate () {
		
		isPressed = false;
		spriteRenderer.flipX = false;
		for (int i = 0; i < objectToActivate.Length; i++) {

			objectToActivate[i].Deactivate();
		}
	}

	private void Update () {
		
		if (isColliding == true && (Input.GetKeyDown(KeyCode.E))) {
			if (isPressed == false) {

				Activate();
			} else {

				Deactivate();
			}
		}
	}

	protected override void OnTriggerEnter2D (Collider2D col) {

		if (col.tag == "Player") {

			hintObject.gameObject.SetActive(true);
			isColliding = true;
		}
	}

	protected override void OnTriggerStay2D (Collider2D col) {}

	protected override void OnTriggerExit2D (Collider2D col) {
		
		if (col.tag == "Player") {

			hintObject.gameObject.SetActive(false);
			isColliding = false;
		}
	}
}
