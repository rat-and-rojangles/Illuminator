using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MultipleChoiceMenu : MonoBehaviour {

	[SerializeField]
	private MultipleChoiceButton [] buttons;

	private int currentIndex = 0;

	[SerializeField]
	private RectTransform particles;

	/// <summary>
	/// Called once to set the start value.
	/// </summary>
	protected abstract int GetInitialValueOfProperty ();

	/// <summary>
	/// sets the property tied to this menu
	/// </summary>
	protected abstract void SetPropertyTiedToIndex (int index);

	void Start () {
		currentIndex = GetInitialValueOfProperty ();
		buttons [currentIndex].selected = true;
		buttons [currentIndex].ParentParticlesToThis (particles);
	}

	public void ChangeActiveButton (int index) {
		if (index != currentIndex) {
			buttons [currentIndex].selected = false;
			buttons [index].selected = true;
			buttons [index].ParentParticlesToThis (particles);
			currentIndex = index;
			SetPropertyTiedToIndex (index);
		}
	}
}
