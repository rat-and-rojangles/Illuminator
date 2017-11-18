using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class PlaneSegment : ScriptableObject {

	/// <summary>
	/// Can this segment be cleared without swapping?
	/// </summary>
	public bool possible;

	// [HideInInspector]
	[SerializeField]
	private BlockColumn [] m_columns;
	public BlockColumn GetColumn (int x) {
		return m_columns [x];
	}
	public int columnCount {
		get { return m_columns.Length; }
	}

#if UNITY_EDITOR
	// first line contains meta
	[TextArea]
	public string csv;

	[ContextMenu ("Read From CSV")]
	public void ReadFromCSV () {
		//
		List<string> lines = new List<string> (csv.Split (null));
		possible = !(lines [0].Contains ("impossible"));
		lines.RemoveAt (0);
		for (int x = lines.Count - 1; x >= 0; x--) {
			if (lines [x].Length == 0) {
				lines.RemoveAt (x);
			}
		}

		bool [,] cells = new bool [lines [0].Split (',').Length, lines.Count];
		for (int y = 0; y < lines.Count; y++) {
			string [] currentRow = lines [y].Split (',');
			Debug.Log (lines [y] + "(" + currentRow.Length + ")");
			for (int x = 0; x < currentRow.Length; x++) {
				if (currentRow [x].Length > 0) {
					cells [x, y] = true;
				}
				else {
					cells [x, y] = false;
				}
			}
		}
		m_columns = new BlockColumn [cells.GetLength (0)];
		for (int x = 0; x < cells.GetLength (0); x++) {
			List<bool> currentColumn = new List<bool> ();
			for (int y = cells.GetLength (1) - 1; y >= 0; y--) {
				currentColumn.Add (cells [x, y]);
			}
			while (currentColumn.Count > 0 && !currentColumn [currentColumn.Count - 1]) {
				currentColumn.RemoveAt (currentColumn.Count - 1);
			}
			m_columns [x] = new BlockColumn (currentColumn.ToArray ());
		}
	}
#endif
}
