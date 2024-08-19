using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeRotation : MonoBehaviour
{
    public float rotationAngle = 90f;
    private Quaternion targetRotation;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            RotatePipe();
    }

    void RotatePipe()
    {
        targetRotation *= Quaternion.Euler(0,0,rotationAngle);
        StartCoroutine(RotateToTarget());
    }

    IEnumerator RotateToTarget()
    {
        float time = 0f;
        float duration = 0.5f;

        while (time < duration)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, time / duration);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
