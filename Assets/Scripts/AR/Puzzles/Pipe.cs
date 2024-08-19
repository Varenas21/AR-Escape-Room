using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    public float rotationAngle = 90;

    public void RotatePipe()
    {
        transform.Rotate(0, 0, rotationAngle);
    }
}
