using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionController : MonoBehaviour {

	[SerializeField] float maxInteractionDistance = 2.0f;
	[SerializeField] GameObject dialogueBoxObject;
	[SerializeField] Image reticle;
	[SerializeField] Sprite defaultReticleImage;
	[SerializeField] Sprite speechBubbleReticleImage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractionDistance)) {
			GameObject hitObject = hit.collider.gameObject;
			if (hitObject.GetComponent<NPCDialogue>()) {
				reticle.sprite = speechBubbleReticleImage;
				if (Input.GetMouseButtonDown (0)) {
					dialogueBoxObject.SetActive(true);
					dialogueBoxObject.SendMessage("DisplayText", hitObject.GetComponent<NPCDialogue>().textToDisplay);
				}
			}
		} else {
			reticle.sprite = defaultReticleImage;
		}
	}
}
