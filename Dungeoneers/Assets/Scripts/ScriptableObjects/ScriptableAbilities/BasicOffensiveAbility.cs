using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Basic Ability",menuName = "ScriptableObjects/Abilities/Basic Ofensive Ability")]
public class BasicOffensiveAbility : Abilities {
	
	public const string PLAYER_PROJECTILE_TAG = "PlayerProjectile";
	
	public override void Initialize() {

		abilityDamage = new Damage (damage, critChance, typeOfDamage, elementOfDamage);

		reach = (((speed.x) + (acceleration.x / 2)) * (disableTime + 1)) + distance;
	}

    public override void TriggerAbility(Transform entity) {
		
		GameObject abilityCast = Multipooling.MultiPool(sprite);
		
		SpriteRenderer thisRenderer = abilityCast.GetComponent<SpriteRenderer>();
		Projectiles thisProjectile = abilityCast.GetComponent<Projectiles>();
		
		abilityCast.tag = PLAYER_PROJECTILE_TAG;
		thisProjectile.speed = ((entity.GetComponent<Rigidbody2D>().velocity) * thisProjectile.momentumMultiplier);

		if (entity.GetComponent<SpriteRenderer>().flipX == false) {

			abilityCast.transform.position = new Vector2(entity.localPosition.x + distance, entity.localPosition.y);
			
			thisProjectile.speed += speed;
			thisProjectile.acceleration = acceleration;

			thisRenderer.flipX = false;

        } else {

			abilityCast.transform.position = new Vector2(entity.localPosition.x - distance, entity.localPosition.y);
			
			thisProjectile.speed += new Vector2(-speed.x, speed.y);
			thisProjectile.acceleration = new Vector2 (-acceleration.x, acceleration.y);

			thisRenderer.flipX = true;
        }

		thisRenderer.color = coloration;
		
		thisProjectile.damage = abilityDamage;
		thisProjectile.disableTime = disableTime;
		
		thisProjectile.Initialize (entity.gameObject, abilityDamage);
    }
}