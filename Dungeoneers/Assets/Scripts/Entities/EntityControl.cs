using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityControl : MonoBehaviour {

	public Characters character;

	protected bool flip = false;

	protected const float HANDS_OFFSET = 0.7f;
	protected const float HANDS_SIZE = 0.05f;
	protected const float FEET_SIZE = 0.15f;
	
	protected float forcaPulo;
	protected float velAndar;

	protected float movX;
	protected float movY;
	protected float velX;
	protected float velY;
	protected float extraX;

	protected bool pulavel = true;
	protected bool pulavelDuplo = false;
	protected bool pulavelParede = false;
	protected bool pulavelVoo = false;

	protected bool noChao = false;
	protected bool segundoPulo = false;
	protected bool naParede = false;

	protected LayerMask whatIsFloor;

	protected GameObject pe; // substituir por raycast
	protected GameObject mao; // substituir por raycast

	protected Rigidbody2D rigid;

	protected SpriteRenderer spriteRenderer;

	protected virtual void Start () {
		
		// Rigidbody
		rigid = GetComponent<Rigidbody2D>();

		// SpriteRenderer
		spriteRenderer = GetComponent<SpriteRenderer>();

		// Define qual a camada do que é chão, também usado para paredes
		whatIsFloor = LayerMask.GetMask("Ground");

		// Instancia um objeto para detectar um chão abaixo do jogador e o posiciona corretamente
		pe = new GameObject("feet");
		pe.transform.parent = gameObject.transform;
		pe.transform.localPosition = new Vector3(0.0f, -0.75f, 0.0f);

		// Instancia um objeto para detectar uma parede à frente do jogador e o posiciona corretamente
		mao = new GameObject("hands");
		mao.transform.parent = gameObject.transform;
		mao.transform.localPosition = new Vector3(0.7f, 0.0f, 0.0f);
		
		// Pega os dados contidos no objeto do personagem e define variáveis
		forcaPulo = character.jumpStrength;
		velAndar = character.movSpd;

		pulavel = character.jump;
		pulavelDuplo = character.jumpTwice;
		pulavelParede = character.jumpWall;
		pulavelVoo = character.jumpFly;
	}

	protected abstract void ControlLateral();

	protected abstract void ControlJump ();

	/// <summary>
	/// Check if the sprite should be mirrowed and mirrors it propperly
	/// </summary>
	protected void ControlMirror () {
		// Espelha o sprite dependendo se o valor for positivo ou negativo
		if (movX < 0) {

			flip = true;
		} else if (movX > 0) {

			flip = false;
		}

		if (flip == true) {

			spriteRenderer.flipX = true;
			mao.transform.localPosition = new Vector2 (-HANDS_OFFSET, 0);
		} else if (flip == false) {

			spriteRenderer.flipX = false;
			mao.transform.localPosition = new Vector2(HANDS_OFFSET, 0);
		}
	}

	/// <summary>
	/// Check if feet are overlapping a ground to detect it under the entity
	/// </summary>
	protected void FeetDetectFloor () {

		// Verifica se o pé está sobrepondo o chão.
		if (Physics2D.OverlapCircle(pe.transform.position, FEET_SIZE, whatIsFloor)) {

			segundoPulo = true;
			noChao = true;
		} else {

			noChao = false;
		}
	}

	/// <summary>
	/// Check if hands are overlapping a wall to detect it in front of the entity
	/// </summary>
	protected void HandsDetectWall () {

		// Verifica se a mão está sobrepondo uma parede.
		if (Physics2D.OverlapCircle(mao.transform.position, HANDS_SIZE, whatIsFloor)) {

			//segundoPulo = true;
			naParede = true;
		} else {

			naParede = false;
		}
	}

	/// <summary>
	/// Workaround made to apply extra horizontal strength after a walljump to avoid straight easy infinite walljumping
	/// </summary>
	protected void ExtraXControl () {
		if (noChao == true) {
			extraX = 0;
		} else if (extraX < 0) {
			extraX += Time.deltaTime;
		} else if (extraX > 0) {
			extraX -= Time.deltaTime;
		}
	}

	/// <summary>
	/// A simple method to regenerate regenerable resources
	/// </summary>
	/// <param name="resource">The actual resource to regen</param>
	/// <param name="resourceMax">The limit of the given resource</param>
	/// <param name="regenRate">The amount of the given resource to regenerate each second (if negative will degenerate)</param>
	/// <returns></returns>
	protected float RegenResources (float resource, float resourceMax, float regenRate) {

		if (resource < resourceMax && resource > 0) {
			return resource += Time.deltaTime * regenRate;
		}else if (resource > resourceMax) {
			return resource = resourceMax;
		}else if (resource < 0) {
			return resource = 0;
		}
		return resource;
	}
}
