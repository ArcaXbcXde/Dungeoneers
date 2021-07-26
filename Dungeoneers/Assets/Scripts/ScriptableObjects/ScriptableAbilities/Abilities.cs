using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abilities : ScriptableObject {
	
	[Header ("Definition")]
    public string abilityName = "ability";

    public TypeOfDamage typeOfDamage = TypeOfDamage.Neutral;

	public ElementOfDamage elementOfDamage = ElementOfDamage.Neutral;

	[Header ("Parameters")]
	[Range (-100.0f, 100.0f)]
    public float damage = 10.0f;

	[Range (0.0f, 100.0f)]
	public float critChance = 0.0f;

	[Range (-100.0f, 100.0f)]
    public float cost = 10.0f;

	[Range (-100.0f, 100.0f)]
    public float burnout = 1.0f;

	[Range (0.0f, 300.0f)]
	public float cooldown = 1.5f;

	[Range (-3.0f, 3.0f)]
    public float distance = 0.5f;

	[Range (0.0f, 60.0f)]
	public float disableTime = 0.0f;
	
	public float reach;

	public Vector2 speed = new Vector2 (0.0f, 0.0f);

	public Vector2 acceleration = new Vector2 (0.0f, 0.0f);

	[Header ("Appearance")]
    public Color coloration = Color.white;

    public Sprite icon;
	
    public GameObject sprite;
	
	[HideInInspector]
	public GameObject user;

	protected Damage abilityDamage;

	public abstract void Initialize();

    public abstract void TriggerAbility(Transform entity);

	protected virtual void OnEnable () {
		
		Initialize();
	}
}