using UnityEngine;
using System.Collections;

public class RandomiseAnimationStartingPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Animator>().Play(0,-1, Random.value);
	}
}