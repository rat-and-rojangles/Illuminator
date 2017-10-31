using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Palette {
	protected abstract Color playerColorProperty { get; }
	public Color playerColor {
		get { return playerColorProperty; }
	}

	protected abstract Color activeBlockColorA { get; }
	protected abstract Color activeBlockColorB { get; }
	public Color activeBlockColor {
		get { return Game.staticRef.planeManager.planeAIsActive ? activeBlockColorA : activeBlockColorB; }
	}

	protected abstract Color primedBlockColorA { get; }
	protected abstract Color primedBlockColorB { get; }
	public Color primedBlockColor {
		get { return Game.staticRef.planeManager.planeAIsActive ? primedBlockColorB : primedBlockColorA; }
	}

	protected abstract Color illuminatedBlockColorA { get; }
	protected abstract Color illuminatedBlockColorB { get; }
	public Color illuminatedBlockColor {
		get { return Game.staticRef.planeManager.planeAIsActive ? illuminatedBlockColorA : illuminatedBlockColorB; }
	}

	protected abstract Color slamWarningColorA { get; }
	protected abstract Color slamWarningColorB { get; }
	public Color slamWarningColor {
		get { return Game.staticRef.planeManager.planeAIsActive ? slamWarningColorB : slamWarningColorA; }
	}

	protected abstract Color backgroundColorA { get; }
	protected abstract Color backgroundColorB { get; }
	public Color backgroundColor {
		get { return Game.staticRef.planeManager.planeAIsActive ? backgroundColorA : backgroundColorB; }
	}
}
