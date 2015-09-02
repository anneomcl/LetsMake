using UnityEngine;
using System.Collections;

public class NPCRecolour : MonoBehaviour {

	[SerializeField]
    private Color color;

	// Use this for initialization
	void Start () {
		GetComponentInChildren<Renderer>().material.color = color;
	}
}
