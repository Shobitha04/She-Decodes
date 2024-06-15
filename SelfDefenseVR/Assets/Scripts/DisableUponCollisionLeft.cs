using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/**
* Attached to each sphere that is on a left path punch, this script will
* disable the current sphere's collider and enable the next's collider.
*/
public class DisableUponCollisionLeft : MonoBehaviour
{
    // get this sphere's collider
    private SphereCollider sphereCollider;

	/**
	* Initialize the sphere's collider.
	*/
	void Start ()
    {
        sphereCollider = this.GetComponent<SphereCollider>();
	}

    /**
    * Returns the Transform of the next sphere in the path.
    */
    private Transform GetNext()
    {
        Transform currentTransform = transform; // current sphere's transform
        Transform parent = transform.parent; // parent object's transform
        var childcount = parent.childCount;
        
        // loop through children until find current sphere is found, then get index of next one
        for (int i = 0; i < childcount - 1; i++)
        {
            if (currentTransform == parent.GetChild(i))
            {
                return parent.GetChild(i + 1);
            }
        }
        return null;
    }

    /**
    * Disables the sphere that is collided by the left hand, and enables the next sphere
    * along with it's collider.
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("leftHand"))
        {
            this.sphereCollider.enabled = false; // disable collider
            this.sphereCollider.gameObject.SetActive(false);
            

            Transform nextSphere = GetNext();
            if (nextSphere != null) {
                nextSphere.GetComponent<SphereCollider>().enabled = true; // enable next sphere collider
            }
           
            
            
        }
    }
}
