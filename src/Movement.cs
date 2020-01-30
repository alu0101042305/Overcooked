using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementa el movimiento de un objeto a través de los ejes vertical y horizontal
public class Movement : MonoBehaviour
{
    public float verticalMove = 0;
    public float horizontalRotation = 0;
    public float moveSpeed = 5; // velocidad de movimiento
    public float rotationSpeed = 2; // velocidad de rotación
    Rigidbody rb; // rigidbody
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        verticalMove = Input.GetAxis("Vertical");
        horizontalRotation = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * verticalMove * moveSpeed;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, horizontalRotation * rotationSpeed, 0) );
    }

}
