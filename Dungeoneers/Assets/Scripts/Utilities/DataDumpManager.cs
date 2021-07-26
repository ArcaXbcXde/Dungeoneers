using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataDumpManager : MonoBehaviour {

	public GameObject dataDumpPrefab; // verificar utilidade

	public Button buttonMale;
	public Button buttonFemale;

	SortCharacters sorter;
	
	private Selections selections;

	private DataDump dump;

	private void Start () {

		dump = DataDump.Instance;

		sorter = GetComponent<SortCharacters>();
		selections = GetComponent<Selections>();

		if (dump.selectedGender == Gender.Female) {

			buttonMale.gameObject.SetActive(false);
			buttonFemale.gameObject.SetActive(true);

		}
	}
	
	public void SetPlayerMale () {
		buttonMale.gameObject.SetActive(true);
		buttonFemale.gameObject.SetActive(false);
		dump.selectedGender = Gender.Male;
		selections.imgSel[0].sprite = selections.UpdateGender(Gender.Male);
		sorter.SetGenderToMale();
	}

	public void SetPlayerFemale () {
		buttonMale.gameObject.SetActive(false);
		buttonFemale.gameObject.SetActive(true);
		dump.selectedGender = Gender.Female;
		selections.imgSel[0].sprite = selections.UpdateGender(Gender.Female);
		sorter.SetGenderToFemale();
	}
}