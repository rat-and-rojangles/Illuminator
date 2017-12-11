using UnityEngine;

public class ControlSchemeManager : MultipleChoiceMenu {
	protected override int GetInitialValueOfProperty () {
		return PlayerRecords.controlsIndex;
	}

	protected override void SetPropertyTiedToIndex (int index) {
		PlayerRecords.controlsIndex = index;
	}
}
