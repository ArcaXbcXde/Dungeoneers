using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayer : CreateEntity {
	
	private GameObject player;

	private DataDump dump;

	private void Awake () {

		dump = DataDump.Instance;

		player = new GameObject("Player " + dump.selectedCharacter.name) {
			tag = "Player",
			layer = 12
		};

		player.transform.position = this.transform.position;
		dump.stagePlayer = player.transform;

		CreateSpriteRenderer(player, dump.selectedCharacter, dump.selectedGender);
		CreateRigidBody(player);
		CreateBoxCollider(player);
	}

	private void Start () {
		
		CreateEntityControl(player);

		CreateEntityResources(player, dump.selectedCharacter);
	}

	protected override void CreateEntityControl (GameObject entity) {

		PlayerControl playerControl = entity.AddComponent<PlayerControl>();
		playerControl.character = dump.selectedCharacter;

		AbilityCD [] UiSkills = dump.stageUI.GetComponentsInChildren<AbilityCD>();
		playerControl.uiSkill1 = UiSkills [0];
		playerControl.uiSkill2 = UiSkills [1];
		playerControl.uiSkill3 = UiSkills [2];
	}

	private void CreateEntityResources (GameObject entity, Characters character) {

		Text[] UIText = dump.stageUI.GetComponentsInChildren<Text>(false); 

		Image[] UIImage = dump.stageUI.GetComponentsInChildren<Image>(false);

		PlayerResources playerResources = entity.AddComponent<PlayerResources>();
		playerResources.txtHp = UIText[0];
		playerResources.txtEn = UIText[1];

		if (dump.selectedGender == Gender.Male) {

			dump.stageUI.GetComponentInChildren<Image>().sprite = dump.selectedCharacter.spriteMale;
		} else {

			dump.stageUI.GetComponentInChildren<Image>().sprite = dump.selectedCharacter.spriteFemale;
		}

		playerResources.BgHp = UIImage[1];
		playerResources.hpBar = UIImage[2];
		playerResources.BgEn = UIImage[3];
		playerResources.enBar = UIImage[4];
		
		playerResources.Initialize(character);
	}
}