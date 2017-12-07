using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputQueryButtons : IPlayerInputQuery {
	public PlayerInputStruct NextInput () {
		PlayerInputStruct l_input = new PlayerInputStruct ();
		l_input.jumpDown = Input.GetButtonDown ("Jump");
		l_input.jumpHeld = Input.GetButton ("Jump");
		l_input.swapDown = Input.GetButtonDown ("Swap");
		return l_input;
	}
}
