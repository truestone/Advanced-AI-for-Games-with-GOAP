using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    Camera cam;
    public float speed = 10.0f;
    public float rotationSpeed = 20.0f;

    void Start() {
        cam = GetComponentInChildren<Camera>();
        cam.transform.LookAt(transform.position);
    }

    void Update()
    {
        float forward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float right = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(right,0, forward);

        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.C))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.R) && cam.transform.position.y > 5)
        {
            cam.transform.Translate(0, 0, speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.F) && cam.transform.position.y < 45)
        {
            cam.transform.Translate(0, 0, -speed * Time.deltaTime);
        }

        float angle = Vector3.Angle(cam.transform.forward, Vector3.up);

        if (Input.GetKey(KeyCode.T) && angle < 175)
        {
            cam.transform.Translate(0, rotationSpeed * Time.deltaTime, 0);
            cam.transform.LookAt(transform.position);
        }

        if (Input.GetKey(KeyCode.G) && angle > 95)
        {
            cam.transform.Translate(0, -rotationSpeed * Time.deltaTime, 0);
            cam.transform.LookAt(transform.position);
        }
    }
}
