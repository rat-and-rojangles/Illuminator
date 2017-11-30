using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralPalette : Palette {
	public ProceduralPalette (float primaryHue, float separationFactor) {
		m_player = new ColorHSV (primaryHue, 1f, 1f, 1f);
		float hueA = (primaryHue - 0.5f + separationFactor).Normalized01 ();
		float hueB = (primaryHue - 0.5f - separationFactor).Normalized01 ();
		m_activeA = new ColorHSV (hueA, 0.75f, 0.5f, 1f);
		m_primedAndBackgroundA = new ColorHSV (hueA, 0.5f, 0.75f, 1f);
		m_illuminatedA = new ColorHSV (hueA, 0.25f, 1f, 1f);
		m_slamA = m_illuminatedA.ChangedAlpha (0.5f);

		m_activeB = new ColorHSV (hueB, 0.5f, 0.75f, 1f);
		m_primedAndBackgroundB = new ColorHSV (hueB, 0.75f, 0.5f, 1f);
		m_illuminatedB = new ColorHSV (hueB, 0.25f, 1f, 1f);
		m_slamB = m_illuminatedB.ChangedAlpha (0.5f);
	}

	// private Color generateActive (float hue) {
	// 	return new ColorHSV (hue, 0.75f, 0.5f, 1f);
	// }

	private Color m_player;
	protected override Color playerColorProperty { get { return m_player; } }

	private Color m_activeA;
	protected override Color activeBlockColorA { get { return m_activeA; } }

	private Color m_activeB;
	protected override Color activeBlockColorB { get { return m_activeB; } }

	private Color m_primedAndBackgroundA;
	protected override Color primedBlockColorA { get { return m_primedAndBackgroundA; } }

	private Color m_primedAndBackgroundB;
	protected override Color primedBlockColorB { get { return m_primedAndBackgroundB; } }

	private Color m_illuminatedA;
	protected override Color illuminatedBlockColorA { get { return m_illuminatedA; } }

	private Color m_illuminatedB;
	protected override Color illuminatedBlockColorB { get { return m_illuminatedB; } }

	private Color m_slamA;
	protected override Color slamWarningColorA { get { return m_slamA; } }

	private Color m_slamB;
	protected override Color slamWarningColorB { get { return m_slamB; } }

	protected override Color backgroundColorA { get { return m_primedAndBackgroundA; } }

	protected override Color backgroundColorB { get { return m_primedAndBackgroundB; } }
}
