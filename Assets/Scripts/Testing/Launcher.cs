using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject _ballPrefab;

    public float _fireRate = 0.5f;

    public float fireForce = 300;

    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer > _fireRate)
        {
            Vector3 launchDir = Camera.main.transform.forward + (Camera.main.transform.up * 0.4f);
            timer = 0;

            var ball = Instantiate(_ballPrefab, Camera.main.transform.position, Quaternion.identity);
            var rb = ball.GetComponent<Rigidbody>();

            rb.AddForce(launchDir*fireForce);
        }
    }
}
