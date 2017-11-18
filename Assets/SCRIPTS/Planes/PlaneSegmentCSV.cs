using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlaneSegmentCSV : ScriptableObject {
#if UNITY_EDITOR
	public string csv;

	public void ReadFromCSV () {

		string [] lines = csv.Split ('\n');
		string [] [] characters = new string [lines.Length] [];

		int maxLength = 0;
		for (int x = 0; x < characters.Length; x++) {
			characters [x] = lines [x].Split (',');
			maxLength = Mathf.Max (maxLength, characters [x].Length);
		}

		for (int lineIndex = 0; lineIndex < characters.Length; lineIndex++) {
			//
		}
	}
#endif

	[System.Serializable]
	public class BlockColumn2 {
		public BlockColumn2 (bool [] blocks) {
			this.blocks = blocks;
		}
		public bool [] blocks;
        
	}

	public BlockColumn2 [] columns;
}
