using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Vector3Util {
	public static bool Approx(Vector3 a, Vector3 b, float delta) {
		return Math.Abs(a.x - b.x) < delta && Math.Abs(a.y - b.y) < delta && Math.Abs(a.z - b.z) < delta;
	}
}