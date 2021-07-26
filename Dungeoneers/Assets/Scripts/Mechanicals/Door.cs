using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Mechanicals {
	
	protected Collider2D collisor;
	protected Animator anim;

	protected override void Start () {

		base.Start();
		collisor = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();
	}
	
	public override void Activate () {

		spriteRenderer.sprite = offStateSprite;
		isPressed = false;
		collisor.enabled = false;
		anim.SetBool ("isClosed", false);
	}

	public override void Deactivate () {

		spriteRenderer.sprite = onStateSprite;
		isPressed = true;
		collisor.enabled = true;
		anim.SetBool("isClosed", true);
	}

	protected override void OnTriggerEnter2D (Collider2D col) {

		if (col.tag == "Player" && isPressed == true) {

			hintObject.gameObject.SetActive(true);
		}
	}

	protected override void OnTriggerStay2D (Collider2D col) {}

	protected override void OnTriggerExit2D (Collider2D col) {
		
		if (col.tag == "Player") {

			hintObject.gameObject.SetActive(false);
		}
	}
}
