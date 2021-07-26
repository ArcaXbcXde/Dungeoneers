using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Character Inventory", menuName = "ScriptableObjects/Inventories/Character Inventory")]
public class CharacterInventory : ScriptableObject {
	
	public List<CharacterSlot> Container = new List<CharacterSlot>();
	
	public CharacterInventory startingCharacters;
	
	public void AddCharacter (Characters _character){
		
		bool unlockedCharacter = false;
		for (int i = 0; i < Container.Count; i++) {

			if (Container[i].character == _character) {

				unlockedCharacter = true;
				break;
			}
		}
		if (unlockedCharacter == false) {

			Container.Add(new CharacterSlot(_character));
		}
	}

	[ContextMenu ("Set Basics")]
	private void SetBasics () {
		Container.Clear();
		for (int i = 0; i < startingCharacters.Container.Count; i++) {

			Container.Add((new CharacterSlot(startingCharacters.Container[i].character)));
		}
	}

	[ContextMenu ("Clear Characters")]
	private void ClearContainer () {

		Container.Clear();
	}

}

[System.Serializable]
public class CharacterSlot {

	public Characters character;
	public CharacterSlot (Characters _character) {
		
		character = _character;
	}
}
