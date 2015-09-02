using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionController : MonoBehaviour {

	[SerializeField]
    private float maxInteractionDistance = 2.0f;

	[SerializeField]
    private GameObject dialogueBoxObject;

	[SerializeField]
    private Image reticle;

	[SerializeField]
    private Sprite defaultReticleImage;

	[SerializeField]
    private Sprite speechBubbleReticleImage;

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
        if (!isAllowedToInteractWith(out hit)) {
            reticle.sprite = defaultReticleImage;
            return;
        }

		GameObject hitObject = hit.collider.gameObject;
        if (hitObject.GetComponent<NPCDialogue>()) {
			//reticle.sprite = speechBubbleReticleImage;
			if (Input.GetMouseButtonDown(0)) {
				dialogueBoxObject.SetActive(true);
				dialogueBoxObject.SendMessage("DisplayText", hitObject.GetComponent<NPCDialogue>().textToDisplay);
			}
		}
	}

    /// <param name="hit">The object the players is allowed to interact with</param>
    private bool isAllowedToInteractWith (out RaycastHit hit) {
        return Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractionDistance);
    }
}
