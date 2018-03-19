using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0;
    public float crouchSpeed = 4f;
    public float jumpSpeed = 0;


    private Animator anim;
    public Rigidbody rb;
    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * moveSpeed;

        motor.Move(_velocity);

        if (Input.GetKey(KeyCode.W))
        {
            moveSpeed = 5.5f;
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalkingBack", false);
            anim.SetBool("Sprinting", false);
            anim.SetBool("Jumping", false);
            Jump();
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            moveSpeed = 15.5f;
            anim.SetBool("isRunning", false);
            anim.SetBool("Sprinting", true);

        }

        if (Input.GetKey(KeyCode.S))
        {
            moveSpeed = crouchSpeed;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalkingBack", true);
            anim.SetBool("Sprinting", false);
            Jump();
        }
        else
        {
            anim.SetBool("isWalkingBack", false);
        }

        
       

        //float _xRot = Input.GetAxisRaw("Mouse X");

        //Vector3 _cameraRotation = new Vector3(0f, _xRot, 0f) * sensitivity;

        //motor.RotateCamera(_cameraRotation);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Jumping ON");
            rb.AddForce(Vector3.up * jumpSpeed);
            anim.SetBool("Jumping", true);
            //anim.SetBool("isRunning", false);
            //anim.SetBool("Sprinting", false);
        }
        else
        {
            anim.SetBool("Jumping", false);
        }
    }
}