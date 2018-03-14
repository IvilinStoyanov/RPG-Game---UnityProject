using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Animator anim;
    [Header("Movement")]
    private float movementSpeed;
    private float rotSpeed = 6f;
    private float crouchSpeed = 0.03f;

    // private float jumpHeight = 200f;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var z = Input.GetAxis("Vertical") * movementSpeed;
        var y = Input.GetAxis("Horizontal") * rotSpeed;

        transform.Translate(0, 0, z);
        transform.Rotate(0, y, 0);

        // Animation event
        if (Input.GetKey(KeyCode.W))
        {
            movementSpeed = 0.08f;
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalkingBack", false);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementSpeed = crouchSpeed;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalkingBack", true);
        }
        else
        {
            anim.SetBool("isWalkingBack", false);
        }
    }
}


