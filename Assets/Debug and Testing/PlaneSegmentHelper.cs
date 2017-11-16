using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlaneSegmentHelper : MonoBehaviour {

	[ContextMenu ("Generate Plane Segment")]
	private void CreatePlaneSegment () {
		int minK = int.MaxValue;
		int maxK = int.MinValue;
		Dictionary<int, List<float>> dic = new Dictionary<int, List<float>> ();
		foreach (Transform child in transform) {
			int key = Mathf.RoundToInt (child.position.x);
			if (key > maxK) {
				maxK = key;
			}
			if (key < minK) {
				minK = key;
			}
			if (!dic.ContainsKey (key)) {
				dic.Add (key, new List<float> ());
			}
			dic [key].Add (child.position.y);
		}
		BlockColumn [] cols = new BlockColumn [maxK - minK + 1];
		for (int x = 0; x < cols.Length; x++) {
			if (dic.ContainsKey (x + minK)) {
				cols [x] = BlockColumn.ConstructNew (dic [x + minK].ToArray ());
			}
			else {
				cols [x] = BlockColumn.ConstructNew (new float [0]);
			}
		}
		PlaneSegment ps = ScriptableObject.CreateInstance<PlaneSegment> ();
		ps.Set (cols);
		ps.RemoveDuplicates ();
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath ("Assets/Prefabs/PlaneSegments/" + gameObject.name + ".asset");
		AssetDatabase.CreateAsset (ps, assetPathAndName);
		AssetDatabase.SaveAssets ();
		EditorGUIUtility.PingObject (ps);

	}
}
