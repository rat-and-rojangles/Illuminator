using UnityEngine;

public class SwapEveryTouch : MonoBehaviour {
	void Update () {
#if UNITY_EDITOR
		if (Input.GetMouseButtonDown (0)) {
#else
		if (Utility.ScreenTappedThisFrame ()) {	
#endif
			Game.staticRef.planeManager.Swap ();
		}
	}
}
