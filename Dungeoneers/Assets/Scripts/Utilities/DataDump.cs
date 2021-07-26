using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDump : MonoBehaviour {
	
	public static DataDump Instance {

		get;
		private set;
	}

	public int actualStage;

	public int lastStageID;

	public float difficultyModifier = 1.0f;

	public List<bool> completedStages;

	public Gender selectedGender;

	public Characters selectedCharacter;
	
	public CharacterInventory inventory;
	
	public Transform stagePlayer;
	
	public Transform stageUI;

	private void Awake () {

		InitialSet();
	}

	private void InitialSet () {

		if (Instance == null) {

			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {

			Destroy(gameObject);
		}
	}
	
	public void CompleteStage (CharacterInventory stageInventory) {
		
		completedStages[actualStage] = true;
		
		CheckAllCompleted();

		for (int i = 0; i < stageInventory.Container.Count; i++) {
			
			inventory.AddCharacter(stageInventory.Container[i].character);
		}

		stageInventory.Container.Clear();

		ScenesControl.Instance.GoToScreen("LevelSelection");
	}

	public void StageFail (CharacterInventory stageInventory) {
		
		stageInventory.Container.Clear();

		ScenesControl.Instance.GoToScreen("LevelSelection");
	}

	private void CheckAllCompleted () {

		if (completedStages.Contains(false) == false || (completedStages[lastStageID] == true)) {
			for (int i = 0; i < completedStages.Count; i++) {

				completedStages[i] = false;

				Debug.Log("Resetting all stages");
			}
		}
	}
}
