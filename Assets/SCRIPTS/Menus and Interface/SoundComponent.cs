using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponent : MonoBehaviour {
	public void PlaySpeedUpSound () {
		SoundCatalog.staticRef.PlaySpeedUpSound ();
	}
}
