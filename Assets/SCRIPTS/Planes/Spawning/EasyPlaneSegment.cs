using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyPlaneSegment : PlaneSegment {
	private static BlockColumn column = new BlockColumn (new bool [] { true, true, true });
	private int m_length;
	public EasyPlaneSegment (int length) {
		m_length = length;
	}
	public bool possible {
		get {
			return true;
		}
	}

	public int columnCount { get { return m_length; } }

	public BlockColumn GetColumn (int x) {
		return column;
	}
}
