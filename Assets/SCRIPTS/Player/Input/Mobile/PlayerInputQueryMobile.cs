using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputQueryMobile : IPlayerInputQuery {
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
		switch (PlayerRecords.controlsIndex) {
			case 1:
				swapInput = new RightSwap ();
				break;
			case 2:
				swapInput = new BottomSwap ();
				break;
			case 3:
				swapInput = new TopSwap ();
				break;
			default:						// zero or glitched
				swapInput = new LeftSwap ();
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
