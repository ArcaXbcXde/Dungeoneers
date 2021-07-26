using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selections : MonoBehaviour {
	
	public Image[] imgSel = new Image [4];

	public Text [] txtSel = new Text [14];

	public Characters selChar;
	
	private DataDump dump;
	
	private void Start () {

		dump = DataDump.Instance;
		
		if (dump.selectedCharacter != null) {

			selChar = dump.selectedCharacter;
		} else {

			dump.selectedCharacter = selChar;
		}
		UpdateText();
	}

	public void ChangeChar (Characters newChar) {

		selChar = newChar;
		dump.selectedCharacter = selChar;

		UpdateText();
	}

	private void UpdateText () {

		// Atualiza a imagem de acordo com o gênero
		imgSel [0].sprite = UpdateGender(dump.selectedGender);

		// Atualiza os ícones e os textos das habilidades de acordo com o personagem escolhido
		if (selChar.ability1 != null) {

			imgSel[1].sprite = selChar.ability1.icon;
			txtSel [11].text = selChar.ability1.abilityName;
		} else {
			imgSel[1].sprite = null;
			txtSel[11].text = "No ability";
		}

		if (selChar.ability2 != null) {

			imgSel [2].sprite = selChar.ability2.icon;
			txtSel [12].text = selChar.ability2.abilityName;
		} else {

			imgSel[2].sprite = null;
			txtSel[12].text = "No ability";
		}

		if (selChar.ability3 != null) {

			imgSel[3].sprite = selChar.ability3.icon;
			txtSel[13].text = selChar.ability3.abilityName;
		} else {

			imgSel[3].sprite = null;
			txtSel[13].text = "No ability";
		}
		// Atualiza todos os textos com as informações do personagem
		txtSel [0].text = "Id: " + selChar.id.ToString();
		txtSel [1].text = selChar.charName;


		txtSel [2].text = selChar.race.ToString();


		txtSel [3].text = selChar.hpMax.ToString();
		txtSel [4].text = selChar.hpRegen.ToString();
		txtSel [5].text = selChar.enMax.ToString();
		txtSel [6].text = selChar.enRegen.ToString();
		txtSel [7].text = selChar.defenses.baseDefense.ToString();
		txtSel [8].text = selChar.defenses.shieldMax.ToString();

		// Velocidade de movimento
		txtSel [9].text = UpdateMoveSpeed(selChar.movSpd);

		// Tipo de pulo
		txtSel [10].text = UpdateJumpType();
	}
	
	public Sprite UpdateGender (Gender chosenGender) {

		if (chosenGender == Gender.Male) {

			return selChar.spriteMale;
		} else if (chosenGender == Gender.Female) {

			return selChar.spriteFemale;
		}
		return null;
	}

	public string UpdateMoveSpeed (float charMovSpd) {

		if (charMovSpd < 6) {

			return "Very slow";
		} else if (charMovSpd < 18) {

			return "Slow";
		} else if (charMovSpd <= 24) {

			return "Normal";
		} else if (charMovSpd > 24) {

			return "Fast";
		} else if (charMovSpd > 35) {

			return "Very fast";
		}
		return "unknown";
	}

	public string UpdateJumpType () {

		if (selChar.jumpFly == true) {

			return "Flying";
		} else if (selChar.jumpWall == true) {

			return "Wall jump";
		} else if (selChar.jumpTwice == true) {

			return "Double jump";
		} else if (selChar.jump == true) {

			if (selChar.jumpStrength == 18.0f) {
				return "Normal";
			} else if (selChar.jumpStrength < 18.0f) {
				return "Low jump";
			} else if (selChar.jumpStrength > 18.0f) {
				return "High jump";
			}

		} else if (selChar.jump == false) {

			return "Can't jump";
		}
		return "unknown";
	}
}

