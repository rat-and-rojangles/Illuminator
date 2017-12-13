using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiator : MonoBehaviour {
	[SerializeField]
	private GameObject [] playerPrefabs;
	public PlayerCharacter CreatePlayer () {
		// todo: equip item
		return GameObject.Instantiate (playerPrefabs [PlayerRecords.physiqueIndex], transform.position, Quaternion.identity).GetComponent<PlayerCharacter> ();
	}
}
