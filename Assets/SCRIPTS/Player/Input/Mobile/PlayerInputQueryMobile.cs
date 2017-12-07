using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputQueryMobile : IPlayerInputQuery {
	public enum ControlScheme { LeftSwap, RightSwap, TopSwap, BottomSwap }
	private interface SwapInput {
		bool swapCondition (Touch t);
	}
	private class LeftSwap : SwapInput {
		public bool swapCondition (Touch t) {
			return t.position.x < Screen.width * 0.5f;
		}
	}
	private class RightSwap : SwapInput {
		public bool swapCondition (Touch t) {
			return t.position.x > Screen.width * 0.5f;
		}
	}
	private class TopSwap : SwapInput {
		public bool swapCondition (Touch t) {
			return t.position.y < Screen.height * 0.5f;
		}
	}
	private class BottomSwap : SwapInput {
		public bool swapCondition (Touch t) {
			return t.position.y > Screen.height * 0.5f;
		}
	}

	private SwapInput swapInput;

	public PlayerInputQueryMobile () {
		ControlScheme cs = ControlScheme.LeftSwap;	// should get this from playerprefs
		switch (cs) {
			case ControlScheme.LeftSwap:
				swapInput = new LeftSwap ();
				break;
			case ControlScheme.RightSwap:
				swapInput = new RightSwap ();
				break;
			case ControlScheme.TopSwap:
				swapInput = new TopSwap ();
				break;
			case ControlScheme.BottomSwap:
				swapInput = new BottomSwap ();
				break;
		}
	}

	public PlayerInputStruct NextInput () {
		PlayerInputStruct l_input = new PlayerInputStruct ();
		foreach (Touch t in Input.touches) {
			if (swapInput.swapCondition (t)) {
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
