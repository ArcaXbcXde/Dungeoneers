using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStage : MonoBehaviour {

	public static EndStage Instance {
		get;
		private set;
	}

	public CharacterInventory charactersInventory;

	private void Awake () {
		
		Instance = this;
	}

	private void OnTriggerEnter2D (Collider2D col) {

		if (col.tag == "Player") {

			DataDump.Instance.CompleteStage(charactersInventory);
		}
	}
}