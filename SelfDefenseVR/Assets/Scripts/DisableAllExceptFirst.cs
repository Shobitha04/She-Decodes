using System.Collections;
using UnityEngine;

/**
* Attached to the path parent. Will disbale all the sphere colliders of the spheres in the path except for the first sphere.
* 
*/
public class DisableAllExceptFirst : MonoBehaviour
{
    private SphereCollider[] sphereColliders;

    /**
    * Initialization of sphere collider array.
    */
    private void Awake()
    {
        sphereColliders = GetComponentsInChildren<SphereCollider>(); // initialize the array
    }
    
    /**
    * Disables all colliders except the first for the spheres upon starting the game on the first frame.
    */
    void Start()
    {
        // disable all colliders except first sphere
        DisableAllSphereCollidersExceptFirst();
    }

    /**
    * Set all the sphere colliders off for each sphere except the first. 
    * This is used to allow the user to collect spheres in order.
    */
    void DisableAllSphereCollidersExceptFirst()
    {
        for (int i = 0; i < sphereColliders.Length; i++)
        {
            if (i == 0)
            {
                sphereColliders[i].enabled = true;
            }
            else
            {
                sphereColliders[i].enabled = false;
            }
        }
    }

    /**
    * Reset the path by enabling all sphere game objects, 
    * and disabling all sphere colliders except the first.
    * This will be executed whenever the path (parent or spheres) is enabled.
    *
    */
    private void OnEnable()
    {
        for (int i = 0; i < sphereColliders.Length; i++) {
            sphereColliders[i].gameObject.SetActive(true);
        }
        DisableAllSphereCollidersExceptFirst();
    }   
}
