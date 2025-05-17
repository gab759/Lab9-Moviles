using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Configuración Básica")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float tiltAmount = 25f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody rb;
    private bool isGrounded;
    private float horizontalInput;
    public static event Action Muerte;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;  
    }

    void Update()
    {
        horizontalInput = Input.acceleration.x;
    }

    void FixedUpdate()
    {
        MoveShip();
    }

    private void MoveShip()
    {
        Vector3 velocity = rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;
        rb.linearVelocity = velocity;

        float tiltZ = horizontalInput * -tiltAmount;
        Quaternion targetRotation = Quaternion.Euler(0, 0, tiltZ);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 5f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo") && !isGrounded)
        {
            isGrounded = true;
            Jump();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            jumpForce = 0;
            Muerte?.Invoke();


        }
    }
    private void Jump()
    {
        Vector3 v = rb.linearVelocity;
        v.y = 0f;
        rb.linearVelocity = v;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        
    }
}
