using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfDamage {

	Neutral,
	None,
	Unknown,

	Piercing,
	Cutting,
	Blunt,

	Explosive,
	Special,
}

public enum ElementOfDamage {

	Neutral,
	None,
	Unknown,

	Water,
	Fire,
	Earth,
	Air,

	Ice,
	Metal,

	Nature,
	Lightning,

	Light,
	Dark,
}

[System.Serializable]
public class Damage {

	public float damageAmount;

	public float critChance;

	public TypeOfDamage damageType;

	public ElementOfDamage damageElement;

	public Damage (float damageValue) {

		damageAmount = damageValue;
		critChance = 0;
		damageType = TypeOfDamage.Neutral;
		damageElement = ElementOfDamage.Neutral;
	}

	public Damage (float damageValue, float crit) {

		damageAmount = damageValue;
		critChance = crit;
		damageType = TypeOfDamage.Neutral;
		damageElement = ElementOfDamage.Neutral;
	}

	public Damage (float damageValue, TypeOfDamage type) {

		damageAmount = damageValue;
		critChance = 0;
		damageType = type;
		damageElement = ElementOfDamage.Neutral;
	}

	public Damage (float damageValue, ElementOfDamage element) {

		damageAmount = damageValue;
		critChance = 0;
		damageType = TypeOfDamage.Neutral;
		damageElement = element;
	}

	public Damage (float damageValue, float crit, TypeOfDamage type) {

		damageAmount = damageValue;
		critChance = crit;
		damageType = type;
		damageElement = ElementOfDamage.Neutral;
	}

	public Damage (float damageValue, float crit, ElementOfDamage element) {

		damageAmount = damageValue;
		critChance = crit;
		damageType = TypeOfDamage.Neutral;
		damageElement = element;
	}

	public Damage (float damageValue, TypeOfDamage type, ElementOfDamage element) {

		damageAmount = damageValue;
		critChance = 0;
		damageType = type;
		damageElement = element;
	}

	public Damage (float damageValue, float crit, TypeOfDamage type, ElementOfDamage element) {

		damageAmount = damageValue;
		critChance = crit;
		damageType = type;
		damageElement = element;
	}
}