using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	private Color m_color = Color.red;

	public Color color {
		get { return m_color; }
	}


	[SerializeField]
	private Material characterMaterial;
	[SerializeField]
	private Material fontMaterial;
	[SerializeField]
	private Material blockMaterial;
	[SerializeField]
	private Material audioMaterial;
	[SerializeField]
	private Material activeMaterial;
	[SerializeField]
	private Material primedMaterial;

	private Stack<SpriteRenderer> spriteRenderers = null;
	private float charHue = 0f;

	private float speed = 5f;

	[SerializeField]
	private Slider hueSlider;
	[SerializeField]
	private Slider speedSlider;

	void Awake () {
		charHue = PlayerPrefs.GetFloat ("CharacterHue", 0f);
		hueSlider.value = charHue;

		speed = PlayerPrefs.GetFloat ("Speed", 5f);
		speedSlider.value = speed;

		m_color = Color.HSVToRGB (charHue, 1f, 1f);
		spriteRenderers = new Stack<SpriteRenderer> ();
		foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer> ()) {
			if (sr.color.a > 0.75f) {
				sr.color = m_color;
				spriteRenderers.Push (sr);
			}
		}
	}

	public void SetHue (float hue) {
		charHue = hue;
		m_color = Color.HSVToRGB (hue, 1f, 1f);
		if (spriteRenderers != null) {
			foreach (SpriteRenderer sr in spriteRenderers) {
				sr.color = m_color;
			}
		}
		characterMaterial.color = m_color;
		characterMaterial.SetColor ("_EmissionColor", m_color * 0.25f);
		fontMaterial.color = m_color;
		blockMaterial.color = m_color;

		activeMaterial.color = Color.HSVToRGB ((hue + 1.0f / 3f).Normalized01 (), 1f, 1f); ;
		activeMaterial.SetColor ("_EmissionColor", activeMaterial.color * 0.25f);
		primedMaterial.color = Color.HSVToRGB ((hue + 2.0f / 3f).Normalized01 (), 1f, 1f);
		primedMaterial.SetColor ("_EmissionColor", primedMaterial.color * 0.5f);
	}

	public void SetSpeed (float s) {
		if (Game.staticRef != null) {
			Game.staticRef.AUTO_SCROLL_RATE = s;
		}
		speed = s;
	}

	public void Commit () {
		audioMaterial.color = m_color.ChangedAlpha (audioMaterial.color.a);
		audioMaterial.SetColor ("_EmissionColor", m_color * 0.25f);
		PlayerPrefs.SetFloat ("CharacterHue", charHue);
		PlayerPrefs.SetFloat ("Speed", speed);
	}
}
