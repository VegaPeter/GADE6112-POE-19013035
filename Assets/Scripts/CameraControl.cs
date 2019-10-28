using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Input.GetAxis("Horizontal") * speed;
        transform.position += transform.up * Input.GetAxis("Vertical") * speed;

        transform.Rotate(Vector3.up, Input.GetAxis("Rotation") * rotationSpeed, Space.World);
    }
}
