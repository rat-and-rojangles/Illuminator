using UnityEngine;

public class MusicManager : MultipleChoiceMenu {
	protected override int GetInitialValueOfProperty () {
		return PlayerRecords.musicIndex;
	}

	protected override void SetPropertyTiedToIndex (int index) {
		PlayerRecords.musicIndex = index;
		MusicMaster.staticRef.SetMusicByIndex (index);
	}
}
