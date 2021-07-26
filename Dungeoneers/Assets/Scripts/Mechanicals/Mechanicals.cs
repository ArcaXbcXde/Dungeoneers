using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mechanicals : MonoBehaviour {

	public bool isPressed = false;

	public Sprite onStateSprite;
	public Sprite offStateSprite;

	public Mechanicals[] objectToActivate = new Mechanicals[1];
	
	protected GameObject hintObject;

	protected SpriteRenderer spriteRenderer;

	protected virtual void Start () {

		spriteRenderer = GetComponent<SpriteRenderer>();
		hintObject = transform.GetChild(0).gameObject;
		hintObject.SetActive(false);
	}

	public abstract void Activate ();

	public abstract void Deactivate ();

	protected abstract void OnTriggerEnter2D (Collider2D col);

	protected abstract void OnTriggerStay2D (Collider2D col);

	protected abstract void OnTriggerExit2D (Collider2D col);

}