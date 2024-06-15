using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryBag : MonoBehaviour
{

    // grabs the original position
    private Vector3 bagOriginalPos;

    // grabs the position component of the punching bag
    private Transform bagCurrentPos;

    // grabs the rigidbody component of the punching bag
    private Rigidbody bagBody;

    
    void Awake()
    {
        // initializes the transforms and of the bag and grabs the original position of the bag
        bagOriginalPos = this.gameObject.GetComponent<Transform>().position;
        bagCurrentPos = this.gameObject.GetComponent<Transform>();
        bagBody = this.gameObject.GetComponent<Rigidbody>();

    }

    private float Squaring(float x)
    {
        // return the x squared
        return Mathf.Pow(x, 2);
    }

    private void FixedUpdate()
    {
        // this finds the magnitude of the Vector3 rePos
        Vector3 rePos = new Vector3();
        float squareX = Squaring(bagOriginalPos.x - bagCurrentPos.position.x);
        float squareZ = Squaring(bagOriginalPos.z - bagCurrentPos.position.z);
        float mag = Mathf.Sqrt(squareX + squareZ);

        // if the bag is not in its original position
        if (bagCurrentPos.position.x != bagOriginalPos.x)
        {
            // normalizes the x in rePos
            rePos.x = (bagOriginalPos.x - bagCurrentPos.position.x) / mag;
        }

        if (bagCurrentPos.position.z != bagOriginalPos.z)
        {
            // normalizes the z in rePos
            rePos.z = (bagOriginalPos.z - bagCurrentPos.position.z) / mag;
        }

        // adds the normalization of rePos to the punching bag
        bagBody.AddForce(rePos);
    }
}
