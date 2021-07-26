using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pooling technique made to manage many different poolable objects simultaneously.
/// </summary>
public class Multipooling : MonoBehaviour {

	/// <summary>
	/// Static dictionary storing every poolable object in the project.
	/// </summary>
	private static Dictionary<GameObject, List<GameObject>> poolingDictionary = new Dictionary<GameObject, List<GameObject>>();

	/// <summary>
	/// Stores every pooled object in the project, and if not enought objects it creates one extra and use it immediately.
	/// </summary>
	/// <param name="pooledObject">Object to be pooled.</param>
	/// <param name="newPosition">Desired position to the object to appear.</param>
	/// <param name="newRotation">Desired rotation to the object to appear.</param>
	/// <returns>Returns an instance of the "pooledObject" parameter.</returns>
	public static GameObject MultiPool (GameObject pooledObject, Vector3 newPosition = default, Quaternion newRotation = default) {

		if (poolingDictionary.ContainsKey(pooledObject) == true) {

			foreach (GameObject obj in poolingDictionary[pooledObject].ToArray()) {

				if (obj == null) {

					poolingDictionary[pooledObject].Remove(obj);
				} else if (obj.activeInHierarchy == false) {

					obj.transform.position = newPosition;
					obj.transform.rotation = newRotation;
					obj.SetActive(true);
					return obj;
				}
			}
			poolingDictionary[pooledObject].Add(Instantiate(pooledObject, newPosition, newRotation));
			return poolingDictionary[pooledObject][poolingDictionary[pooledObject].Count - 1];
		}

		poolingDictionary.Add(pooledObject, new List<GameObject>());
		poolingDictionary[pooledObject].Add(Instantiate(pooledObject, newPosition, newRotation));
		return poolingDictionary[pooledObject][poolingDictionary[pooledObject].Count - 1];
	}

	/// <summary>
	/// Adds an already existing object into the pooling dictionary
	/// </summary>
	/// <param name="objectToAdd">Object to be added in the pooling dictionary</param>
	public static void AddOnPool (GameObject objectToAdd) {

		if (poolingDictionary.ContainsKey(objectToAdd) == false) {

			poolingDictionary.Add(objectToAdd, new List<GameObject>());
		}

		poolingDictionary[objectToAdd].Add(objectToAdd);
	}
}