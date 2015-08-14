using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pos = transform.position;

		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			pos.z++;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			pos.z--;
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			pos.x--;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			pos.x++;
		}

		transform.position = pos;
	}
}
