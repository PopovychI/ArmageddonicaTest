using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    void Update()
    {
        if (!Input.anyKey) return; //removes unneccesarry drag
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

 
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        MoveCamera(moveDirection);
    }

    void MoveCamera(Vector3 moveDirection)
    {
        var cameraTransform = transform;
        Vector3 newPosition = transform.position + _moveSpeed * Time.deltaTime * moveDirection;
        cameraTransform.position = newPosition;
    }
}
