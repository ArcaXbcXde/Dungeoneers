using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour {

	public bool destroyOnCollision = false;

	[HideInInspector]
	public float disableTime;

	[HideInInspector]
	public Damage damage;

	[Range(-3.0f, 5.0f)]
	public float momentumMultiplier = 1;

	[HideInInspector]
	public GameObject attacker;

	[HideInInspector]
	public Vector2 speed;
	
	[HideInInspector]
	public Vector2 acceleration;

	private Rigidbody2D rigid;
	public void Initialize (GameObject entity, Damage abilityDamage) {
		
		damage = new Damage (abilityDamage.damageAmount, abilityDamage.critChance, abilityDamage.damageType, abilityDamage.damageElement);

		attacker = entity;
	}

	private void Update () {

		speed += acceleration * Time.deltaTime;
		rigid.velocity = speed;
	}
	
	private void OnEnable () {

		rigid = GetComponent<Rigidbody2D>();

		if (disableTime != 0) {

			Invoke("Disable", disableTime);
		}
	}

	public void OnTriggerEnter2D (Collider2D col) {
		if (attacker == null) {

			Disable();
		} else if (col.tag == "Ground" || (col.GetComponent<EntityResources>() != null && col.tag != attacker.tag)) {

			if (col.GetComponent<EntityResources>() != null) {
				
				col.GetComponent<EntityResources>().TakeDamage(damage);
			}
			if (destroyOnCollision == true) {

				Disable();
			}
		}
	}

	private void Disable () {

		gameObject.SetActive(false);
	}
}