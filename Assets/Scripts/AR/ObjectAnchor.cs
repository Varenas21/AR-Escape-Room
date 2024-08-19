using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ObjectAnchor : MonoBehaviour
{
    public ARAnchorManager anchorManager;
    private ARAnchor anchor;

    private void Start()
    {
        Pose anchorPose = new Pose(transform.position, transform.rotation);
        anchor = anchorManager.AddAnchor(anchorPose);
    }

    private void Update()
    {
        if (anchor != null)
        {
            transform.position = anchor.transform.position;
            transform.rotation = anchor.transform.rotation;
        }
    }
}
