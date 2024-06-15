using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagLimit : MonoBehaviour {
    // stores the position of the frame's y
    private float yPos;
	
    // rigidBody of the hanging punching bag
    private Rigidbody bagBody;
	
    // tracks whether the punching bag is going down or not
    private bool reversed = false;

    // grabs the y axis of the hanging punching bag
    void Awake () {
        yPos = GameObject.Find("Cylinder.003").GetComponent<Transform>().position.y;
        bagBody = this.gameObject.GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {

        // if the punching bag goes above the top
        if(this.gameObject.GetComponent<Transform>().position.y >= yPos && !reversed)
        {
            // make the punching bag go the other direction
            Vector3 bagvelocity = new Vector3();
            bagvelocity = bagBody.velocity;
            bagBody.velocity = -(bagvelocity/2);
            reversed = true;

        } else if (this.gameObject.GetComponent<Transform>().position.y < yPos && reversed)
        {
            // when the bag goes under the frame the bag can be pushed back if it goes above
            reversed = false;
        }
    }
}
