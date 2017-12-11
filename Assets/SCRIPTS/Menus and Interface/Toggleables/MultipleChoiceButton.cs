using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class MultipleChoiceButton : MonoBehaviour {

	[SerializeField]
	private Button button;
	[SerializeField]
	public Outline outline;
	[SerializeField]
	private Image image;

	public bool selected {
		get { return !button.interactable; }
		set {
			button.interactable = !value;
			if (selected) {
				image.material = Game.staticRef.planeManager.illuminatedMaterial;
				image.color = Color.white;
				outline.enabled = true;
			}
			else {
				image.material = Game.staticRef.planeManager.activeMaterial;
				image.color = Color.white.ChangedAlpha (0.5f);
				outline.enabled = false;
			}
		}
	}

	public void ParentParticlesToThis (RectTransform rt) {
		rt.SetParent (transform);
		rt.SetAsFirstSibling ();
		rt.anchoredPosition = Vector2.zero;
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
