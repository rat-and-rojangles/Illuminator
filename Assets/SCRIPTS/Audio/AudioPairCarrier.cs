using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPairCarrier : MonoBehaviour {
	public AudioPair audioPair;
	public void SetSoundAsMusic () {
		MusicMaster.staticRef.audioPair = audioPair;
	}
}
