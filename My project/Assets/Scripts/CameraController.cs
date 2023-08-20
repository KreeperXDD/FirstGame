using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed = 10.0f;
    private float _multiplySpeed = 1.0f;
    public float speed = 10.0f;
    public float zoomSpeed = -100.0f;
    
    private void Update()   
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        float rotate = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            rotate = -1f;
        }
        else if(Input.GetKey(KeyCode.E))
        {
            rotate = 1f;
        }

        _multiplySpeed = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate *_multiplySpeed, Space.World);
        transform.Translate(new Vector3(horizontal,0,vertical) * Time.deltaTime * _multiplySpeed * speed, Space.Self);
        transform.position += transform.up * zoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -20.0f, 25.0f),
            transform.position.z);
    }
} 
 