using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Mechanicals {
	
	public override void Activate () {

		if (isPressed == false) {

			isPressed = true;
			spriteRenderer.sprite = onStateSprite;
			for (int i = 0; i < objectToActivate.Length; i++) {

				objectToActivate[i].Activate();
			}
		}
	}

	public override void Deactivate () {

		isPressed = false;
		spriteRenderer.sprite = offStateSprite;
		for (int i = 0; i < objectToActivate.Length; i++) {

			objectToActivate[i].Deactivate();
		}
	}

	protected override void OnTriggerEnter2D (Collider2D col) {

		if (col.tag == "Player" && (isPressed == false)) {

			hintObject.gameObject.SetActive(true);
		}
	}

	protected override void OnTriggerStay2D (Collider2D col) {

		if ((col.tag == "Player") && (Input.GetKeyDown(KeyCode.E))) {

			if (isPressed == false) {

				Activate();
			}
		}
	}

	protected override void OnTriggerExit2D (Collider2D col) {

		if (col.tag == "Player") {

			hintObject.gameObject.SetActive(false);
		}
	}
}