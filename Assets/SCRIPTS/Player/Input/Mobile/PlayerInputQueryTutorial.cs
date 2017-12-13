using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputQueryTutorial : MonoBehaviour, IPlayerInputQuery {

	IPlayerInputQuery realQuery;

	public PlayerInputStruct NextInput () {
		return realQuery.NextInput ();
	}

	void Start () {
#if UNITY_EDITOR
		realQuery = new PlayerInputQueryButtons ();
#else
		realQuery = new PlayerInputQueryMobile ();
#endif
	}
}
