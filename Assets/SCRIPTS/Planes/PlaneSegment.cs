using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlaneSegment : MonoBehaviour {
	[SerializeField]
	private float m_width;
	public float width {
		get { return m_width; }
	}

	public float leftEdge {
		get { return transform.position.x; }
	}
	public float rightEdge {
		get { return leftEdge + width; }
	}

	[SerializeField]
	/// <summary>
	/// Does this belong to planeA?
	/// </summary>
	private bool m_isPlaneA;

	/// <summary>
	/// All blocks in this plane segment. Should be handled as a prefab
	/// </summary>
	public Stack<Block> allBlocks = new Stack<Block> ();

	/// <summary>
	/// The plane this segment is a part of.
	/// </summary>
	public Plane plane {
		get { return m_isPlaneA ? Game.staticRef.planeManager.planeA : Game.staticRef.planeManager.planeB; }
	}
	/// <summary>
	/// Establishes the plane this is part of. Setup only.
	/// </summary>
	public void SetPlane (bool isPlaneA) {
		m_isPlaneA = isPlaneA;
	}

	[SerializeField]
	private bool m_possible;
	/// <summary>
	/// Can the player cross this without flipping planes?
	/// </summary>
	public bool possible {
		get { return m_possible; }
	}

	private void SetTransformZ (float value) {
		transform.position += Vector3.forward * (-transform.position.z + value);
	}

	public void ApplyState () {
		if (plane.state == PlaneState.Active) {
			SetTransformZ (1f);
		}
		else {
			SetTransformZ (0f);
		}
		foreach (Block b in allBlocks) {
			b.state = plane.state;
		}
	}

	[ContextMenu ("Auto Calculate Width")]
	private void AutoCalculateWidth () {
		float max = Mathf.NegativeInfinity;
		float min = Mathf.Infinity;
		foreach (Renderer r in GetComponentsInChildren<Renderer> ()) {
			min = Mathf.Min (r.bounds.min.x, min);
			max = Mathf.Max (r.bounds.max.x, max);
		}
		m_width = Mathf.RoundToInt (max - min);
	}

#if UNITY_EDITOR
	[ContextMenu ("Replace blocks with proxies")]
	private void Proxy () {
		GameObject blockPrefab = Resources.Load ("BlockProxy") as GameObject;

		Block [] oldBlocks = GetComponentsInChildren<Block> ();
		foreach (Block b in oldBlocks) {
			GameObject temp = PrefabUtility.InstantiatePrefab (blockPrefab) as GameObject;
			temp.transform.parent = this.transform;
			temp.transform.localPosition = b.transform.localPosition;

			DestroyImmediate (b.gameObject);
		}
	}

	[ContextMenu ("Replace proxies with blocks")]
	private void ReverseProxy () {
		GameObject blockPrefab = Resources.Load ("Block") as GameObject;

		BlockProxy [] oldBlocks = GetComponentsInChildren<BlockProxy> ();
		foreach (BlockProxy b in oldBlocks) {
			GameObject temp = PrefabUtility.InstantiatePrefab (blockPrefab) as GameObject;
			temp.transform.parent = this.transform;
			temp.transform.localPosition = b.transform.localPosition;

			DestroyImmediate (b.gameObject);
		}
	}

	[ContextMenu ("Update proxies")]
	private void ProxyUpdate () {
		GameObject blockPrefab = Resources.Load ("BlockProxy") as GameObject;

		BlockProxy [] oldBlocks = GetComponentsInChildren<BlockProxy> ();
		foreach (BlockProxy b in oldBlocks) {
			GameObject temp = PrefabUtility.InstantiatePrefab (blockPrefab) as GameObject;
			temp.transform.parent = this.transform;
			temp.transform.localPosition = b.transform.localPosition;

			DestroyImmediate (b.gameObject);
		}
	}
#endif
}
