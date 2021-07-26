using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityResources : MonoBehaviour {

	[HideInInspector]
	public bool invencivel = true;

	[HideInInspector]
	public float hpMax;
	[HideInInspector]
	public float hpRegen;
	public float hp;
	[HideInInspector]
	public float enMax;
	[HideInInspector]
	public float enRegen;
	public float en;
	[HideInInspector]
	public float shieldMax;
	public float shield;
	public float def;

	public virtual void Initialize(Characters character) {
		
		hpMax = character.hpMax;
		hpRegen = character.hpRegen;
		enMax = character.enMax;
		enRegen = character.enRegen;
		shieldMax = character.defenses.shieldMax;
		def = character.defenses.baseDefense;

		hp = hpMax;
		en = enMax;
		shield = shieldMax;
	}

	protected abstract void Update ();

	protected float RegenResources (float resource, float max, float regen) {

		if (resource < max) {

			if (resource < 0) {

				resource = 0;
			}

			resource += regen * Time.deltaTime;
		} else {
			resource = max;
		}

		return resource;
	}

	public virtual void TakeDamage (Damage damageProperties) {
		
		if (damageProperties.damageAmount < 0) {
			hp -= damageProperties.damageAmount;
		} else {
			if (shield > 0) {

				shield -= 1;
			} else if (damageProperties.damageAmount > def) {

				hp -= damageProperties.damageAmount - def;
			}
		}
	}

	protected abstract void OnTriggerEnter2D (Collider2D col);
}