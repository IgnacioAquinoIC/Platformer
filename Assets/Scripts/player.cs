using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float speed = 10f;
    public float dashSpeed = 20f;
    enum DashDirection
    {
        Left, Right, NoDirection
    }
    private DashDirection dashDirection;
    private DashDirection requestedDirection;
    public float dashDuration;
    public float dashTimer = 0f;
    public float dashCooldown = 10f;
    private float cooldownTimer;
    public float jump = 10f;
    bool canJump = false;
    //public bool canJumpRotation = false; Nope

    //bool canDash = false; Legacy solution for dashing
    bool dashRequested = false;
    public Transform cameraTransform; // Camera-based input system
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Debug.Log("PlayerMovement started");
        Debug.Log(rigidbody);
        dashDirection = DashDirection.NoDirection;
        cooldownTimer = dashCooldown;

        cameraTransform = GetComponentInChildren<Camera>().transform;
        Debug.Log(cameraTransform);
    }

    void Update()
    {
        if (Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            if (Keyboard.current.aKey.IsPressed())
            {
                requestedDirection = DashDirection.Left;
                dashRequested = true;
            }

            else if (Keyboard.current.dKey.IsPressed())
            {
                requestedDirection = DashDirection.Right;
                dashRequested = true;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //rigidbody.linearVelocity = Vector3.zero; // Dash-related, this didn't work. oops
        //if (Keyboard.current.wKey.IsPressed())
            //{
                //rigidbody.AddForce(Vector3.forward * Time.fixedDeltaTime * speed, ForceMode.Impulse);
            //} Legacy code from a 3D movement test—now ignored, as this is a 2D project

        if (cooldownTimer < dashCooldown)
        {
            cooldownTimer += Time.fixedDeltaTime;
        }

        if (Keyboard.current.aKey.IsPressed())
            {
                rigidbody.AddForce(-cameraTransform.right * Time.fixedDeltaTime * speed, ForceMode.Impulse); // Switched to camera input method because of rotating platforms
            }

        if (dashRequested && cooldownTimer >= dashCooldown)
        {
            dashRequested = false;
            dashDirection = requestedDirection;
            dashTimer = 0f;
            cooldownTimer = 0f;
        }
        else if (dashRequested && cooldownTimer < dashCooldown)
        {
            dashRequested = false;
        }

        //if (Keyboard.current.sKey.IsPressed())
        //{
        //rigidbody.AddForce(Vector3.back * Time.fixedDeltaTime * speed, ForceMode.Impulse);
        //} Legacy code from a 3D movement test—now ignored, as this is a 2D project

        if (Keyboard.current.dKey.IsPressed())
            {
                rigidbody.AddForce(cameraTransform.right * Time.fixedDeltaTime * speed, ForceMode.Impulse); // Switched to camera input method because of rotating platforms
            }

        //if (Keyboard.current.dKey.IsPressed() && Keyboard.current.shiftKey.wasPressedThisFrame && cooldownTimer >= dashCooldown)
        //{
            //dashDirection = DashDirection.Right;
            //dashTimer = 0f;
            //cooldownTimer = 0f;
        //} Old bs that didn't work well for dash

        if (dashDirection != DashDirection.NoDirection)
        {
            if (dashTimer >= dashDuration)
            {
                dashDirection = DashDirection.NoDirection;
                dashTimer = 0;
                rigidbody.linearVelocity = Vector3.zero;
            }

                else
                {
                    dashTimer += Time.deltaTime;
                        if (dashDirection == DashDirection.Left)
                        {
                            rigidbody.linearVelocity = -cameraTransform.right * dashSpeed;
                        }

                        if (dashDirection == DashDirection.Right)
                        {
                            rigidbody.linearVelocity = cameraTransform.right * dashSpeed;
                        }
            }
        }

        if (Keyboard.current.spaceKey.IsPressed())
        {
            if (canJump)
            {
                canJump = false;
                transform.SetParent(null);
                rigidbody.AddForce(Vector3.up * Time.fixedDeltaTime * jump, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            canJump = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
               if (contact.normal.y > 0.5f)
                {
                    canJump = true;
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            canJump = false;
        }
    }
}