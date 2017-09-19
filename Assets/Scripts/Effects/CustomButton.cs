using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Like a normal button, but better, because I made it and know it 100%.
/// </summary>
public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	[SerializeField]
	private UnityEvent m_OnMouseEnter;
	[SerializeField]
	private UnityEvent m_OnMouseExit;
	[SerializeField]
	private UnityEvent m_OnClick;

	/// <summary>
	/// "enabled"
	/// </summary>
	public bool isReceivingEvents = true;

	private void InvokeIfValid (UnityEvent e) {
		if (isReceivingEvents && e != null) {
			e.Invoke ();
		}
	}

	void OnMouseEnter () {
		InvokeIfValid (m_OnMouseEnter);
	}
	void OnMouseExit () {
		InvokeIfValid (m_OnMouseExit);
	}
	void OnMouseDown () {
		InvokeIfValid (m_OnClick);
	}

	public void OnPointerEnter (PointerEventData eventData) {
		InvokeIfValid (m_OnMouseEnter);
	}

	public void OnPointerExit (PointerEventData eventData) {
		InvokeIfValid (m_OnMouseExit);
	}

	public void OnPointerClick (PointerEventData eventData) {
		InvokeIfValid (m_OnClick);
	}
}
