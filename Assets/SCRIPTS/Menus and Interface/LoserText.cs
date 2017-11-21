using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoserText : MonoBehaviour {
	[SerializeField]
	private string [] loserMessages;

	[SerializeField]
	private Text loserMessage;

	[SerializeField]
	private Text illuminated;
	void OnEnable () {
		loserMessage.text = loserMessages.RandomElement ();
		// illuminated.fontSize = Mathf.RoundToInt (loserMessage.cachedTextGenerator.fontSizeUsedForBestFit * 0.5f);
		illuminated.text = "illuminated: " + Game.staticRef.scoreCounter.illuminatedCount;
	}
}
