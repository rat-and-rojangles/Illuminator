using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshJitter : MonoBehaviour {
	private MeshFilter meshFilter;

	private Vector3 [] baseVertices;

	[SerializeField]
	private float strength = 0.025f;

	[SerializeField]
	private float probability = 0.5f;

	private bool m_jittering = false;
	public bool jittering {
		get { return m_jittering; }
		set {
			if (!value && m_jittering) {
				for (int x = 0; x < baseVertices.Length; x++) {
					meshFilter.mesh.vertices [x] = baseVertices [x];
				}
			}
			m_jittering = value;
		}
	}

	void Awake () {
		meshFilter = GetComponent<MeshFilter> ();
		meshFilter.mesh.MarkDynamic ();
		baseVertices = meshFilter.mesh.vertices.Clone () as Vector3 [];
	}

	void OnDisable () {
		meshFilter.mesh.vertices = baseVertices;
	}

	void FlatShade () {
		Mesh mesh = meshFilter.mesh;

		//Process the triangles
		Vector3 [] oldVerts = mesh.vertices;
		int [] triangles = mesh.triangles;
		Vector3 [] vertices = new Vector3 [triangles.Length];
		for (int i = 0; i < triangles.Length; i++) {
			vertices [i] = oldVerts [triangles [i]];
			triangles [i] = i;
		}
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();
		meshFilter.mesh = mesh;
	}

	//[ContextMenu ("lel")]
	void Update () {
		Vector3 [] points = new Vector3 [baseVertices.Length];
		for (int x = 0; x < baseVertices.Length; x++) {
			if (Random.value <= probability) {
				points [x] = baseVertices [x] + Random.insideUnitSphere * strength;
			}
			else {
				points [x] = baseVertices [x];
			}
		}

		meshFilter.mesh.vertices = points;
		//meshFilter.mesh.RecalculateNormals ();
	}
}
