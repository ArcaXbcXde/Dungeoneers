using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	private bool pause = false;

	public GameObject pauseScreen;

	private void Awake () {
		
		DataDump.Instance.stageUI = transform;
	}

	private void Update () {

		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {

			TogglePause();
		}
	}

	public void TogglePause () {

		pause = !pause;

		if (pause == true) {

			pauseScreen.SetActive(true);
			Time.timeScale = 0;
		} else if (pause == false) {

			pauseScreen.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void PauseGame () {

		pause = true;

		pauseScreen.SetActive(true);
		Time.timeScale = 0;
	}

	public void UnpauseGame () {

		pause = false;

		pauseScreen.SetActive(false);
		Time.timeScale = 1;
	}
}