using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlaneSegment {
	bool possible { get; }
	BlockColumn GetColumn (int x);
	int columnCount { get; }
}
