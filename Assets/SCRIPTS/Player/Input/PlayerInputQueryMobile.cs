using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputQueryMobile : IPlayerInputQuery {
	public PlayerInputStruct nextInput () {
		PlayerInputStruct l_input = new PlayerInputStruct ();
		foreach (Touch t in Input.touches) {
			if (t.position.x < Screen.width * 0.5f) {
				l_input.swapDown = t.phase == TouchPhase.Began;
			}
			else {
				l_input.jumpDown = t.phase == TouchPhase.Began;
				l_input.jumpHeld = l_input.jumpDown || t.phase == TouchPhase.Stationary || t.phase == TouchPhase.Moved;
			}
		}
		return l_input;
	}

}
