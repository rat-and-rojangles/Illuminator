using UnityEngine;

public class ColorblindButton : ToggleButton {
	protected override string activeText {
		get {
			return "colorblind is on";
		}
	}

	protected override string inactiveText {
		get {
			return "colorblind is off";
		}
	}

	protected override bool GetInitialValueOfProperty () {
		return PlayerRecords.colorblind;
	}

	protected override void SetPropertyValue (bool value) {
		PlayerRecords.colorblind = value;
	}
}
