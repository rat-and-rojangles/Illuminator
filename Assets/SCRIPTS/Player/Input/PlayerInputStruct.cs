using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInputStruct {
	/// <summary>
	/// True the frame the jump input was pressed.
	/// </summary>
	public bool jumpDown;
	/// <summary>
	/// True when the jump input is down. Will always be true when jumpDown is true.
	/// </summary>
	public bool jumpHeld;
	/// <summary>
	/// True the frame the swap input is pressed.
	/// </summary>
	public bool swapDown;
}
