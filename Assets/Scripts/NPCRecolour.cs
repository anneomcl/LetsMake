using UnityEngine;
using System.Collections;

public class NPCRecolour : MonoBehaviour {
	[SerializeField] Color color;
	// Use this for initialization
	void Start () {
		GetComponentInChildren<Renderer>().material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
