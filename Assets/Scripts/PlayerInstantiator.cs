using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates the player at the beginning of the game. Instantiates at this location.
/// </summary>
public class PlayerInstantiator : MonoBehaviour {
	[SerializeField]
	private GameObject [] playerPrefabs;

	void Awake () {
		int playerIndex = PlayerPrefs.GetInt ("PlayerTypeIndex");
		try {
			GameObject.Instantiate (playerPrefabs [playerIndex], transform.position, Quaternion.identity, transform.parent);
		}
		catch (System.ArgumentOutOfRangeException) {
			GameObject.Instantiate (playerPrefabs [0], transform.position, Quaternion.identity, transform.parent);
		}
		GameObject.Destroy (this.gameObject);
	}
}
