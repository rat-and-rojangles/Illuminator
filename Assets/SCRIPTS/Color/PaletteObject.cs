using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A set of colors for all objects in the game.
/// </summary>
public class PaletteObject : ScriptableObject {	// should also implement palette, but i'm not fucking with that rn
	[SerializeField]
	private Color m_player;
	[SerializeField]
	private Color m_activeA;
	[SerializeField]
	private Color m_primedA;
	[SerializeField]
	private Color m_illuminatedA;
	[SerializeField]
	private Color m_slamWarningA;
	[SerializeField]
	private Color m_backgroundA;
	[SerializeField]
	private Color m_activeB;
	[SerializeField]
	private Color m_primedB;
	[SerializeField]
	private Color m_illuminatedB;
	[SerializeField]
	private Color m_slamWarningB;
	[SerializeField]
	private Color m_backgroundB;


	public Color playerColor {
		get { return m_player; }
	}
	public Color activeBlockColor {
		get { return Game.staticRef.planeManager.planeAIsActive ? m_activeA : m_activeB; }
	}
	public Color primedBlockColor {
		get { return Game.staticRef.planeManager.planeAIsActive ? m_primedB : m_primedA; }
	}
	public Color illuminatedBlockColor {
		get { return Game.staticRef.planeManager.planeAIsActive ? m_illuminatedA : m_illuminatedB; }
	}
	public Color slamWarningColor {
		get { return Game.staticRef.planeManager.planeAIsActive ? m_slamWarningB : m_slamWarningA; }
	}
	public Color backgroundColor {
		get { return Game.staticRef.planeManager.planeAIsActive ? m_backgroundA : m_backgroundB; }
	}
}
