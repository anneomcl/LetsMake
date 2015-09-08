using UnityEngine;
using System.Collections;

public class CompanionMovement : MonoBehaviour {

    private Transform playerTransform;
    public float regularSpeed;
    public float catchUpSpeed;
    public float distanceLimitMin;
    public float distanceLimitMax;

    void Start () {
		GameObject playerObject = GameObject.FindWithTag("Player");
		if (playerObject != null) {
            playerTransform = playerObject.GetComponent<Transform>();
		}
	}

	void Update () {	
		transform.LookAt(playerTransform.position);
        if (isPlayerShortDistanceAway()) {
            transform.Translate(Vector3.forward * Time.deltaTime * regularSpeed);
        } else if (isPlayerFarAway()) {
            transform.Translate(Vector3.forward * Time.deltaTime * catchUpSpeed);
        }
	}

    private bool isPlayerShortDistanceAway () {
        return Vector3.Distance(playerTransform.position, transform.position) > distanceLimitMin && Vector3.Distance(playerTransform.position, transform.position) <= distanceLimitMax;
    }

    private bool isPlayerFarAway () {
        return Vector3.Distance(playerTransform.position, transform.position) > distanceLimitMax;
    }
}
