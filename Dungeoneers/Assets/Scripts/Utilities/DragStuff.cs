using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragStuff : MonoBehaviour, IDragHandler, IPointerDownHandler {

	// x min, x max, y min, y max
	public float[] limits = new float[4];

	private Vector3 difference;
	private Vector2 oldMousePos = new Vector2 (0, 0);

	private readonly PointerEventData dataEvent = null;

	public void PointerDown () {
		OnPointerDown (dataEvent);
	}

	public void OnPointerDown (PointerEventData eventData) {

		difference = new Vector3(Input.mousePosition.x - transform.localPosition.x, Input.mousePosition.y - transform.localPosition.y, 0);
	}

	public void OnDrag (PointerEventData eventData) {

		float posX = 0;
		float posY = 0;

		Vector2 newMousePos = Input.mousePosition;
		Vector2 mouseDifference = newMousePos - oldMousePos;
		oldMousePos = newMousePos;

		posX = Input.mousePosition.x - difference.x;
		posY = Input.mousePosition.y - difference.y;
		


		if (transform.localPosition.x <= limits[0] && mouseDifference.x < 0) {
			
			posX = limits[0];
			OnPointerDown(eventData);
		} else if (transform.localPosition.x >= limits[1] && mouseDifference.x > 0) {

			posX = limits[1];
			OnPointerDown(eventData);
		}

		if (transform.localPosition.y <= limits[2] && mouseDifference.y < 0) {

			posY = limits[2];
			OnPointerDown(eventData);
		} else if (transform.localPosition.y >= limits[3] && mouseDifference.y > 0) {

			posY = limits[3];
			OnPointerDown(eventData);
		}

		transform.localPosition = new Vector2(posX, posY);
	}
}
