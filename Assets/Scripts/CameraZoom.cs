using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Input.GetAxis("Mouse ScrollWheel") * speed; 
    }
}
