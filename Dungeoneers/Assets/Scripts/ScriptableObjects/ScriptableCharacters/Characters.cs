using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race {
	Human = 0,
	Humanoid = 7,
	Holy = 3,
	Unholy = 4,
	Undead = 2,
	Animal = 1,
	Mythical = 5,
	Elemental = 6,
	Unknown = 8,

}

public enum Gender {
	Male,
	Female
}


public abstract class Characters : ScriptableObject {

	[System.Serializable]
	public struct Defenses {

		[Range(0.0f, 10.0f)]
		public float baseDefense;

		[Range(0.0f, 10.0f)]
		public int shieldMax;
		
		[Range(-10.0f, 10.0f)]
		public float pierce;
		[Range(-10.0f, 10.0f)]
		public float cut;
		[Range(-10.0f, 10.0f)]
		public float blunt;

		[Range(-10.0f, 10.0f)]
		public float explosive;

		[Range(-10.0f, 10.0f)]
		public float water;
		[Range(-10.0f, 10.0f)]
		public float fire;
		[Range(-10.0f, 10.0f)]
		public float earth;
		[Range(-10.0f, 10.0f)]
		public float air;
	}

	[Header("Definition")]
	public Sprite spriteMale;
	public Sprite spriteFemale;
	
	[Min (0)]
	public int id = 0;
	[Min (0)]
	public int pos = 0;

	[Header("Personal info")]
	public string charName = "personagem";

    public Race race = Race.Human;
	
	[Header("HP")]
	[Range(1.0f, 500.0f)]
	public float hpMax = 25.0f;
	[Range(0.0f, 20.0f)]
	public float hpRegen = 0.2f;
	
	public Defenses defenses;

	[Header("Energy")]
	[Range(0.0f, 1000.0f)]
	public float enMax = 20.0f;
	[Range(0.0f, 100.0f)]
	public float enRegen = 10.0f;
	
	[Header("Movement")]
	[Range (0.0f, 50.0f)]
	[InspectorName("Speed")]
	public float movSpd = 20.0f;
	[Range(0.0f, 50.0f)]
	public float jumpStrength = 18.0f;

    public bool jump = true;
    public bool jumpTwice = false;
    public bool jumpWall = false;
    public bool jumpFly = false;

	[Header("Abilities")]
	public Abilities ability1;
    public Abilities ability2;
    public Abilities ability3;

    public abstract void Initialize(GameObject obj);

	#region context menus
	[ContextMenu ("Set to initial values")]
	private void SetInitialValues () {

		hpMax = 25.0f;
		hpRegen = 0.2f;
		enMax = 20.0f;
		enRegen = 10.0f;
		defenses.shieldMax = 0;
		defenses.baseDefense = 0.0f;
		movSpd = 20.0f;
		jumpStrength = 18.0f;
	}

	[ContextMenu ("Clean Skills")]
	private void CleanSkills () {

		ability1 = null;
		ability2 = null;
		ability3 = null;
	}

	[ContextMenu ("Wipe values")]
	private void WipeValues () {
		SetInitialValues();
		CleanSkills();
		id = 0;
		pos = 0;
		charName = "character";
		race = Race.Human;
		jump = true;
		jumpFly = false;
		jumpTwice = false;
		jumpWall = false;
	}
	#endregion
}
