using UnityEngine;

public class TutorialButton : ToggleButton {
	protected override string activeText {
		get {
			return "tutorial is on";
		}
	}

	protected override string inactiveText {
		get {
			return "tutorial is off";
		}
	}

	protected override bool GetInitialValueOfProperty () {
		return PlayerRecords.tutorial;
	}

	protected override void SetPropertyValue (bool value) {
		PlayerRecords.tutorial = value;
	}
}
