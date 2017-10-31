using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public int levelIndex;

	// Use this for initialization
	void Start () {
		StartCoroutine (Load ());
	}
	IEnumerator Load () {
#if UNITY_IPHONE
            Handheld.SetActivityIndicatorStyle(iOS.ActivityIndicatorStyle.Gray);
#elif UNITY_ANDROID
		Handheld.SetActivityIndicatorStyle (AndroidActivityIndicatorStyle.Small);
#elif UNITY_TIZEN
            Handheld.SetActivityIndicatorStyle(TizenActivityIndicatorStyle.Small);
#endif

		Handheld.StartActivityIndicator ();
		yield return new WaitForSeconds (0);
		SceneManager.LoadScene (1);
	}


}
