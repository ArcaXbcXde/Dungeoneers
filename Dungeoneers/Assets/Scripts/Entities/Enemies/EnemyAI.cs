using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlertState {
	Normal,
	Indiferent,

	Sleeping,
	Fleeing,
	Scared,

	Guarding,
	Alert,
	Investigating,
	Pursuing
}

public abstract class EnemyAI : EntityControl {

	#region public variables

	public float BurnoutCoolRate = 60.0f;

	public float difficultyModifier = 1.0f;

	public AlertState alertState = AlertState.Normal;

	public CharacterInventory inventory;

	public GameObject hpBar;

	public readonly float DISTANCE_TO_STOP = 1.0f;

	#endregion

	#region protected variables

	protected const float VIEW_ANGLE = 90.0f;

	protected const float VIEW_DISTANCE = 10.0f;

	protected float alertLevel = 0.0f;
	protected float timeToAlert = 1.0f;
	protected float timeToStopInvestigating = 5.0f;
	protected float timeInvestigating = 0.0f;

	protected float timeToStop = 0.0f;

	protected float[] cdSkill = new float[3] { 0.0f, 0.0f, 0.0f };
	
	protected float burnout;

	protected float rightOrLeft = 0.5f;

	protected bool seePlayer = false;
	protected bool jumpWill = false;

	protected Abilities[] ability = new Abilities[3];
	
	protected EnemyResources resources;

	protected Transform lastPlayerSeenPosition;

	protected GameObject player;

	#endregion

	#region basics
	protected override void Start () {
		
		base.Start();

		// Procura na cena qual objeto possui seus recursos
		resources = GetComponent<EnemyResources>();

		// Diz quem é o jogador
		player = GameObject.FindGameObjectWithTag("Player");

		// Define suas habilidades
		resources.Initialize(character);
		
		ability[0] = character.ability1;
		ability[1] = character.ability2;
		ability[2] = character.ability3;
		
		// Conta todos os cooldowns
		StartCoroutine (CountCDs());

	}

	protected virtual void FixedUpdate () {

		// Check if player is visible
		DetectPlayer();

		// Detecta se tem chão embaixo
		FeetDetectFloor();

		// Detecta se tem parede à frente
		HandsDetectWall();

	}

	protected virtual void Update () {

		// Controla movimento lateral
		if (alertState != AlertState.Alert) {
			ControlLateral();
		}

		// Controla o pulo
		ControlJump();

		// Se está sem hp, morre
		if (resources.hp <= 0) {

			OnDeath();
		}
	}

	protected virtual void LateUpdate () {

		// Controla o estado de alerta do inimigo
		ControlState();

		// Controla o espelhamento
		ControlMirror();
		
		// Controle da barra de hp
		hpBar.transform.localScale = new Vector2((resources.hp / resources.hpMax), 1);
		
		// Regenera os recursos
		RegenResources(resources.hp, resources.hpMax, resources.hpRegen);
		RegenResources(resources.en, resources.enMax, resources.enRegen);

		// Controle de velocidade horizontal de outras fontes
		if (extraX != 0) {
			ExtraXControl();
		}
		
		// Por fim executa o movimento
		rigid.velocity = new Vector2(velX, velY);
	}
	#endregion
	
	#region enemy's behavioral movement
	protected override void ControlLateral () {

		if (alertState == AlertState.Normal) {

			Roam();
		} else if (alertState == AlertState.Alert){

			Watch();
		} else if (alertState == AlertState.Investigating) {

			Investigate();
		} else if (alertState == AlertState.Pursuing) {

			Pursue ();
		}
		
		if (naParede == false) {

			velX = (movX * velAndar) + (extraX * forcaPulo * 3);
		} else {

			velX = 0;
		}
	}

	protected virtual void Roam () {

		if (timeToStop <= 0) {

			rightOrLeft = UnityEngine.Random.value;
			timeToStop = UnityEngine.Random.Range(1.0f, 2.0f);
		} else if (timeToStop < 0.8f) {

			rightOrLeft = 0.5f;
			movX = 0.0f;
		}

		if ((rightOrLeft < 0.3)) {

			movX = UnityEngine.Random.Range(0.05f, 0.1f);
		} else if ((rightOrLeft > 0.6)) {

			movX = UnityEngine.Random.Range(-0.05f, -0.1f);
		}
	}

	protected virtual void Watch () {

		flip = (transform.position.x < player.transform.position.x) ? false : true;
		/*
		 * if (player.transform.position.x > transform.position.x){
		 * 
		 *		flip = false;
		 * } else {
		 * 
		 *		flip = true;
		 * }
		 */
	}

	protected virtual void Investigate () {

		float investigatingRandomPos = 0.5f;
		float maintainRandom = 0.5f;

		// Movimenta o inimigo
		if (transform.position.x < lastPlayerSeenPosition.position.x - 2.0f) {
			movX = 1.0f;
		} else if (transform.position.x > lastPlayerSeenPosition.position.x + 2.0f) {
			movX = -1.0f;
		} else {
			movX = 0.0f;
		}

		// Faz o inimigo ficar se movimentando ao ficar alterando o último lugar que o jogador foi visto
		if (transform.position.x > lastPlayerSeenPosition.position.x + 2.1
			&& transform.position.x < lastPlayerSeenPosition.position.x - 2.1
			&& maintainRandom != investigatingRandomPos) {
			
			investigatingRandomPos = UnityEngine.Random.value;
			maintainRandom = investigatingRandomPos;
		}
		if (investigatingRandomPos < 0.5f) {

			lastPlayerSeenPosition.position = new Vector2 (lastPlayerSeenPosition.position.x - (2 * investigatingRandomPos), lastPlayerSeenPosition.position.y);
		} else if (investigatingRandomPos > 0.5f) {

			lastPlayerSeenPosition.position = new Vector2 (lastPlayerSeenPosition.position.x + (2 * investigatingRandomPos), lastPlayerSeenPosition.position.y);
		}
	}

	protected virtual void Pursue () {

		Watch();

		float distanceFromPlayer = DistanceFromPlayer();

		// Movimenta o inimigo

		if (DistanceXFromPlayer() > DISTANCE_TO_STOP) {

			if (transform.position.x < lastPlayerSeenPosition.position.x) {
				movX = 1.0f;
			} else if (transform.position.x > lastPlayerSeenPosition.position.x) {
				movX = -1.0f;
			}
		} else if (DistanceXFromPlayer() < DISTANCE_TO_STOP && DistanceYFromPlayer() > 0.8f && DistanceYFromPlayer() < 1.0f) {

			if (transform.position.x < lastPlayerSeenPosition.position.x) {
				movX = -1.0f;
			} else if (transform.position.x > lastPlayerSeenPosition.position.x) {
				movX = 1.0f;
			}
		} else {
			movX = 0.0f;
		}
		
		// Controla o ataque do inimigo
		if (burnout <= 0) {
			if (ability[0] != null && cdSkill[0] <= 0 && SkillUsable(ability[0]) == true) {
				
				ControlAttack(ability[0]);
				cdSkill[0] = ability[0].cooldown * difficultyModifier;
			} else if (ability[1] != null && cdSkill[1] <= 0 && SkillUsable(ability[1]) == true) {
				
				ControlAttack(ability[1]);
				cdSkill[1] = ability[1].cooldown * difficultyModifier;
			} else if (ability[2] != null && cdSkill[2] <= 0 && SkillUsable(ability[2]) == true) {
				
				ControlAttack(ability[2]);
				cdSkill[2] = ability[2].cooldown * difficultyModifier;
			}
		}
	}

	protected override void ControlJump () {

		// Se o inimigo quiser pular, verifica se é cabível
		if (ControlJumpWill()) {

			if ((pulavelVoo == true) || (noChao == true)) {

				velY = forcaPulo;

			} else if ((pulavelParede == true) && (naParede == true)) {

				if (flip == false) {

					extraX = -0.5f;
					velY = forcaPulo;
				} else if (flip == true) {

					extraX = 0.5f;
					velY = forcaPulo;
				}

			} else if ((noChao == false) && (pulavelDuplo == true) && (segundoPulo == true)) {

				velY = forcaPulo;
				segundoPulo = false;
			} else if (movX != 0 && velY == 0 && naParede == false && noChao == true) {

				velY = 3.0f;
			} else {

				velY = rigid.velocity.y;
			}
		} else if (movX != 0 && velY == 0 && naParede == false && noChao == true) {

			velY = 3.0f;
		} else {

			velY = rigid.velocity.y;
		}
	}
	#endregion

	#region AI handling
	protected virtual void DetectPlayer () {

		Vector2 normalizedDistance = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;

		Debug.DrawRay(transform.position, normalizedDistance * VIEW_DISTANCE, Color.blue);

		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, normalizedDistance, VIEW_DISTANCE);

		if (hitInfo == true) {

			PlayerControl playerControlSeen = hitInfo.transform.GetComponent<PlayerControl>();
			if (playerControlSeen != null) {

				lastPlayerSeenPosition = hitInfo.transform;
				seePlayer = true;
			} else {

				seePlayer = false;
			}
		}
	}

	protected float DistanceFromPlayer () {
		
		return Vector2.Distance(transform.position, player.transform.position);
	}

	protected float DistanceYFromPlayer () {

		return Math.Abs(transform.position.y - player.transform.position.y);
	}

	protected float DistanceXFromPlayer () {

		return Math.Abs(transform.position.x - player.transform.position.x);
	}

	protected virtual void ControlState () {

		if (seePlayer == true) {

			if (alertLevel < timeToAlert) {

				alertLevel += Time.deltaTime / (DistanceFromPlayer() / VIEW_DISTANCE);
				if ((alertState == AlertState.Normal) || (alertState == AlertState.Investigating)) {

					alertState = AlertState.Alert;
				}
			} else if (alertLevel >= timeToAlert) {

				alertState = AlertState.Pursuing;
			}
			
		} else {

			if (alertLevel > 0) {

					alertLevel -= Time.deltaTime;
			}else if (alertLevel < 0) {

				alertLevel = 0;
				timeInvestigating = timeToStopInvestigating;
				alertState = AlertState.Investigating;
			}

			if (timeInvestigating > 0) {

				timeInvestigating -= Time.deltaTime;
			} else if (timeInvestigating < 0) {

				timeInvestigating = 0;
				alertState = AlertState.Normal;
			}
		}
	}

	protected virtual bool ControlJumpWill () {

		if (
			((alertState == AlertState.Pursuing) && ((player.transform.position.y - 2.0f) > transform.position.y))
			|| (naParede == true && (
									(alertState == AlertState.Pursuing)
									|| (alertState == AlertState.Investigating)
									))
			|| (alertState == AlertState.Fleeing)
			) {
			return true;
		}
		return false;
	}

	protected virtual void ControlAttack (Abilities thisAbility) {
		resources.en -= thisAbility.cost;
		burnout += thisAbility.burnout * difficultyModifier;

		thisAbility.TriggerAbility(transform);
	}

	protected virtual bool SkillUsable (Abilities thisAbility) {
		
		if ((thisAbility.reach) > DistanceFromPlayer()
				&& (thisAbility.cost) < (resources.en)
				) {

			return true;
		}
		return false;
	}
	#endregion
	
	private IEnumerator CountCDs () {

		while (true) {

			if (timeToStop > 0) {

				timeToStop -= 0.1f;
			}

			if (cdSkill[0] > 0) {

				cdSkill[0] -= 0.1f;
			}

			if (cdSkill[1] > 0) {

				cdSkill[1] -= 0.1f;
			}

			if (cdSkill[2] > 0) {

				cdSkill[2] -= 0.1f;
			}

			if (burnout > 0) {

				burnout -= 0.1f * BurnoutCoolRate;
			} else if (burnout < 0) {
				burnout = 0;
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	public void OnDeath () {
		
		inventory.AddCharacter(character);
		Destroy(gameObject);
	}
}
