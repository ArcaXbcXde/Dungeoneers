using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiType {

	Special,
	Pacific,
	Semipacific,
	Neutral,
	Territorial,
	Agressive,
	Superagressive,
	Dumb,
}

public enum GenerationType {
	ByTrait,
	ByCharacter,
}

public class CreateEnemy : CreateEntity {

	public Traits trait;

	public Characters character;

	public CharacterInventory inventory;

	public GenerationType generation = GenerationType.ByCharacter;

	public AiType typeOfAI = AiType.Agressive;

	public GameObject hpBar;
	private GameObject newBar;
	
	private void Awake () {

		if (generation == GenerationType.ByTrait) {
			
			character = trait.traitedEntities[Random.Range(0, trait.traitedEntities.Count)];
		}
		
		GameObject enemy = new GameObject(character.name);

		newBar = Instantiate(hpBar);
		newBar.transform.parent = enemy.transform;
		newBar.transform.localPosition = new Vector2 (0, 0.9f);
		
		enemy.tag = "Enemy";
		enemy.layer = 13;
		enemy.transform.position = this.transform.position;

		CreateSpriteRenderer(enemy, character, RandomGender());

		CreateRigidBody(enemy);

		CreateBoxCollider(enemy);

		CreateEntityControl(enemy);

		DefineResources (enemy);
	}

	protected override void CreateEntityControl (GameObject entity) {

		EnemyAI enemyAI;

		if (typeOfAI == AiType.Pacific) {

			enemyAI = entity.AddComponent<PacificAI>();
			DefineAIVariables(enemyAI);
		} else if (typeOfAI == AiType.Semipacific) {

			enemyAI = entity.AddComponent<SemiPacificAI>();
			DefineAIVariables(enemyAI);
		} else if (typeOfAI == AiType.Neutral) {

			enemyAI = entity.AddComponent<NeutralAI>();
			DefineAIVariables(enemyAI);
		} else if (typeOfAI == AiType.Territorial) {

			enemyAI = entity.AddComponent<TerritorialAI>();
			DefineAIVariables(enemyAI);
		} else if (typeOfAI == AiType.Agressive) {

			enemyAI = entity.AddComponent<AgressiveAI>();
			DefineAIVariables(enemyAI);
		} else if (typeOfAI == AiType.Superagressive) {

			enemyAI = entity.AddComponent<SuperAgressiveAI>();
			DefineAIVariables(enemyAI);
		} else if (typeOfAI == AiType.Special){

			enemyAI = entity.AddComponent<SpecialAI>();
			DefineAIVariables(enemyAI);
		} else {
			enemyAI = entity.AddComponent<DumbAI>();
			DefineAIVariables(enemyAI);
		}

	}

	private void DefineAIVariables (EnemyAI ai) {

		ai.inventory = inventory;

		ai.character = character;

		ai.hpBar = newBar.transform.GetChild(0).gameObject;

		ai.difficultyModifier = DataDump.Instance.difficultyModifier;
	}

	private void DefineResources (GameObject entity) {

		EnemyResources enemyResources = entity.AddComponent<EnemyResources>();

		enemyResources.Initialize(character);
	}

	private Gender RandomGender () {

		int random = Random.Range(1, 3);
		
		if (random == 2) {

			return Gender.Female;
		}else {
			
			return Gender.Male;
		}
	}
}
