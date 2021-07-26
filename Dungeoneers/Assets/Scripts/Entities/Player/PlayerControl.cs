using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : EntityControl {

	public AbilityCD uiSkill1;
	public AbilityCD uiSkill2;
	public AbilityCD uiSkill3;

	[HideInInspector]
	public PlayerResources resources;

	#region basics
	protected override void Start () {

		base.Start();

		// Procura na cena qual objeto possui seus recursos
		resources = FindObjectOfType<PlayerResources>();

		// Define suas habilidades
		resources.Initialize(character);

		if (character.ability1 != null) {

			uiSkill1.Initialize(character.ability1, resources);
		} else {

			uiSkill1.gameObject.SetActive(false);
		}

		if (character.ability2 != null) {

			uiSkill2.Initialize(character.ability2, resources);
		} else {

			uiSkill2.gameObject.SetActive(false);
		}
		if (character.ability3 != null) {

			uiSkill3.Initialize(character.ability3, resources);
		} else {

			uiSkill3.gameObject.SetActive(false);
		}
	}

	// Updates
	private void FixedUpdate () {
		
		// Detecta se tem chão embaixo
		FeetDetectFloor();

		// Detecta se tem parede à frente
		HandsDetectWall();
	}
	
	private void Update () {

		if (resources.hp <= 0) {

			gameObject.SetActive(false);
			Invoke("Defeat", 3);
		}
		// Controla movimento lateral
		ControlLateral();

		// Controla o pulo
		ControlJump();

		// Controla o espelhamento
		ControlMirror();
	}

	private void LateUpdate () {

		// Controle de velocidade horizontal de outras fontes
		if (extraX != 0) {
			ExtraXControl();
		}

		// Por fim, executa o movimento
		rigid.velocity = new Vector2(velX, velY);
	}
	#endregion

	protected override void ControlLateral () {

		movX = Input.GetAxis("Horizontal");
		// Define o movimento horizontal através do eixo horizontal pré-definido, se estiver fora da parede.
		if (naParede == false) {
			velX = (movX * velAndar) + (extraX * forcaPulo * 3);
		} else {
			velX = 0;
		}
	}

	protected override void ControlJump () {

		// Ao pressionar uma tecla de pulo, verifica se o personagem atual pula, vendo se ele pula normal, se tem pulo duplo, etc.
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && pulavel == true) {

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

			}
		} else {

			velY = rigid.velocity.y;
		}
	}
}
