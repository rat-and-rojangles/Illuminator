using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPropGenerator : MonoBehaviour {

	[SerializeField]
	private GameObject [] props;

	private float sinceLast = -1f;


	// Update is called once per frame
	void Update () {
		if (sinceLast <= 0f && Random.value < 0.01f) {
			GameObject go = Instantiate (props.RandomElement (), transform.position, Quaternion.Euler (0f, Random.value * 360f, 0f));
			go.transform.parent = Game.staticRef.worldTransform;
			Destroy (go, 10f);
			sinceLast = Random.value * 3f;
		}
		else {
			sinceLast -= Time.deltaTime;
		}
	}
}
