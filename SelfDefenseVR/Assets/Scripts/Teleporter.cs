using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Provides Gaze teleportation to the user. Attached to a controller.
*/
public class Teleporter : MonoBehaviour {
    public GameObject TeleportMarker; // teleport reticle displayed on ground
    public Transform Player;
    public float RayLength = 50f; // how far the user can see the teleport reticle
    public OVRInput.Controller Controller;

	
	/**
	* Enables the teleport reticle when the user holds the "B" button on the right controller.
	* If the user also pulls the trigger, then they will be teleported to the place the
	* ray casted from the player's center eye hits the ground. This method is called every frame.
	*/
	void Update () {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, RayLength)) {
            if(hit.collider.tag == "Ground" && OVRInput.Get(OVRInput.Button.Two, Controller)) {
                if (!TeleportMarker.activeSelf) {
                    TeleportMarker.SetActive(true);
                }
                TeleportMarker.transform.position = hit.point;

                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, Controller)) {
                     Player.position = new Vector3(hit.point.x, Player.position.y, hit.point.z);
                }
            } else {
                TeleportMarker.SetActive(false);
            }
        } else {
            TeleportMarker.SetActive(false);
        }

	}


}
