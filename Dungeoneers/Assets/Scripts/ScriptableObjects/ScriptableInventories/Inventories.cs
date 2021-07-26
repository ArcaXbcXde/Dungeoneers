using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Inventories : ScriptableObject {

	public abstract void AddItem (Items _item, int amount);

	public abstract void AddCharacter(Characters _character);
}

