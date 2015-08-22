using UnityEngine;
using System.Collections;

// Lock the cursor with every click. You'll want to disable this script if you add, for example, a menu.
public class MouseLock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
