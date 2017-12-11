using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ToggleButton : MonoBehaviour {
	[SerializeField]
	private Button button;
	[SerializeField]
	public Outline outline;
	[SerializeField]
	private Image image;
	[SerializeField]
	private Text text;

	private bool m_activated;

	[SerializeField]
	private GameObject particles;

	public bool activated {
		get { return m_activated; }
		set {
			m_activated = value;
			SetPropertyValue (value);
			if (activated) {
				image.material = Game.staticRef.planeManager.illuminatedMaterial;
				image.color = Color.white;
				outline.enabled = true;
				particles.SetActive (true);
				text.text = activeText;
			}
			else {
				image.material = Game.staticRef.planeManager.activeMaterial;
				image.color = Color.white.ChangedAlpha (0.5f);
				outline.enabled = false;
				particles.SetActive (false);
				text.text = inactiveText;
			}
		}
	}

	/// <summary>
	/// Called once to set the start value.
	/// </summary>
	protected abstract bool GetInitialValueOfProperty ();

	/// <summary>
	/// sets the property tied to this menu
	/// </summary>
	protected abstract void SetPropertyValue (bool value);

	protected abstract string activeText { get; }
	protected abstract string inactiveText { get; }

	/// <summary>
	/// Establish initial value
	/// </summary>
	void Start () {
		m_activated = GetInitialValueOfProperty ();
		activated = m_activated;
	}

	public void Toggle () {
		activated = !activated;
	}

#if UNITY_EDITOR
	[ContextMenu ("Fill out fields")]
	private void FillFields () {
		button = GetComponentInChildren<Button> ();
		image = button.GetComponent<Image> ();
		outline = GetComponentInChildren<Outline> ();
		outline.enabled = false;
	}
#endif
}
