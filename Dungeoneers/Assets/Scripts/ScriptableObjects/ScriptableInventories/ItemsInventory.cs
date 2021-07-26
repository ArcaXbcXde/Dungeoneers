using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
	Equipment,
	Consumable
}

[CreateAssetMenu(fileName = "Items Inventory", menuName = "ScriptableObjects/Inventories/Items Inventory")]
public class ItemsInventory : ScriptableObject {

	public GameObject prefab;

	public ItemType itemType;

	public List<ItemSlot> Container = new List<ItemSlot>();

	public void AddItem (Items _item, int _amount) {

		bool hasItem = false;

		for (int i = 0; i < Container.Count; i++) {

			if (Container[i].item == _item) {

				Container[i].AddAmount(_amount);
				hasItem = true;
				break;
			}
		}

		if (hasItem == false){
			Container.Add (new ItemSlot(_item, _amount));
		}
	}
	
}

[System.Serializable]
public class ItemSlot {

	public Items item;
	public int amount;
	public ItemSlot (Items _item, int _amount) {

		item = _item;
		amount = _amount;
	}

	public void AddAmount (int value) {

		amount += value;
	}
}