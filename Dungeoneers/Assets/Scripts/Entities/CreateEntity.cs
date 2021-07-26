using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreateEntity : MonoBehaviour {
	
	/// <summary>
	/// The physics to be added on Rigibody to all entities created by this method
	/// </summary>
	public PhysicsMaterial2D physicsMaterial;

	/// <summary>
	/// A method to add a Sprite Renderer to a new object entity
	/// </summary>
	/// <param name="entity">The entity to have the Sprite Renderer added</param>
	/// <param name="character">The Character scriptableObject to get the sprite from</param>
	/// <param name="gender">If the sprite of the character to be added is a male or female variant</param>
	protected virtual void CreateSpriteRenderer (GameObject entity, Characters character, Gender gender) {

		SpriteRenderer spriteRenderer = entity.AddComponent<SpriteRenderer>();
		
		if (gender == Gender.Male) {

			spriteRenderer.sprite = character.spriteMale;
		} else if (gender == Gender.Female) {

			spriteRenderer.sprite = character.spriteFemale;
		}
	}
	
	/// <summary>
	/// A method to add a Rigidbody and ititialize its values to a new object entity
	/// </summary>
	/// <param name="entity">The entity to have the Rigidbody added</param>
	protected virtual void CreateRigidBody (GameObject entity) {

		Rigidbody2D rigid = entity.AddComponent<Rigidbody2D>();

		rigid.sharedMaterial = physicsMaterial;
		rigid.gravityScale = 5.0f;
		rigid.freezeRotation = true;
	}

	/// <summary>
	/// A method to add a Box Collider and ititialize its values to a new object entity
	/// </summary>
	/// <param name="entity">The entity to have the Box Collider added</param>
	protected virtual void CreateBoxCollider (GameObject entity) {

		BoxCollider2D boxCollider = entity.AddComponent<BoxCollider2D>();

		boxCollider.offset = new Vector2(0.0f, -0.0625f);
		boxCollider.size = new Vector2(0.5f, 0.875f);
	}

	/// <summary>
	/// An abstract method to define what will be the behavior controlling the entity object
	/// </summary>
	/// <param name="entity">The entity to have control added</param>
	protected abstract void CreateEntityControl (GameObject entity);
}