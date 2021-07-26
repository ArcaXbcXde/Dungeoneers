using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResources : EntityResources {
	
	protected override void Update () {
		
		// Se nesse frame o inimigo ainda não morreu, regenera
		if (hp > 0) {

			hp = RegenResources(hp, hpMax, hpRegen);
			en = RegenResources(en, enMax, enRegen);
		}
	}
	
	protected override void OnTriggerEnter2D (Collider2D col) { }
}
