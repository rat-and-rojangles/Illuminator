using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInputQuery {
	/// <summary>
	/// Returns the input for this frame and resets the input. Call only once per frame
	/// </summary>
	PlayerInputStruct nextInput ();
}
