using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "ScriptableObjects/Trait")]
public class Traits : ScriptableObject {

	public string traitName;

	public List<Characters> traitedEntities;
}