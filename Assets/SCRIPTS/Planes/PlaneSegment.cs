using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class PlaneSegment : ScriptableObject {

	[SerializeField]
	private BlockColumn [] m_columns;
	public BlockColumn GetColumn (int x) {
		return m_columns [x];
	}
	public int columnCount {
		get { return m_columns.Length; }
	}

#if UNITY_EDITOR
	public void Set (BlockColumn [] columns) {
		m_columns = columns;
	}

	[MenuItem ("Assets/Create/Plane Segment Helper")]
	public static void CreateHelperObject () {
		GameObject parent = new GameObject ();
		parent.name = Selection.objects [0].name;
		parent.AddComponent<PlaneSegmentHelper> ();

		PlaneSegment ps = Selection.objects [0] as PlaneSegment;
		for (int x = 0; x < ps.columnCount; x++) {
			foreach (float y in ps.GetColumn (x).yPositionsRevealed) {
				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cube.transform.position = new Vector3 (x, y, 0);
				cube.transform.parent = parent.transform;
			}
		}
	}
	[MenuItem ("Assets/Create/Plane Segment Helper", true)]
	public static bool ValidateCreateHelperObject () {
		if (Selection.objects.Length != 1 || !(Selection.objects [0] is PlaneSegment)) {
			return false;
		}
		else {
			return true;
		}
	}

	[ContextMenu ("Remove Duplicates")]
	public void RemoveDuplicates () {
		foreach (BlockColumn b in m_columns) {
			b.RemoveDuplicates ();
		}
	}

	[ContextMenu ("Remove End Gap")]
	public void RemoveEndGap () {
		List<BlockColumn> tempColumns = new List<BlockColumn> (m_columns);
		while (tempColumns [tempColumns.Count - 1].yPositionsRevealed.Length == 0) {
			tempColumns.RemoveAt (tempColumns.Count - 1);
		}
	}

#endif
}
