using System;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/*
 *  VALERIA ARENAS 2024
 *  AR ESCAPE ROOM
 */

/*
 * Detect the user touch i.e when user taps on the screen.
 * Project a raycast
 * Instantiate a virtual cube at the point where raycast meets the detected plane
 */

public class ARObjectPlacement : MonoBehaviour
{
    public XROrigin ARSessionOrigin;
    public List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    public GameObject cube; // TESTING
    GameObject instantiatedCube;

    void Update()
    {
        if(Input.GetMouseButton(0)) // This also represents tap like mouse tap. Boolean return
        {
            bool collision = ARSessionOrigin.GetComponent<ARRaycastManager>().Raycast(Input.mousePosition, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);
           
            if (collision)
            {
                if(instantiatedCube == null)  {instantiatedCube = Instantiate(cube);}

                instantiatedCube.transform.position = raycastHits[0].pose.position;
            }
        }
   
    }
}
