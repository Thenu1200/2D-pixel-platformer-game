using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour


{
    public Animator anim;

    private CharacterController controller;
    // Start is called before the first frame update

    public float moveSpeed = 10f;
    public float jumpSpeed = 10f;
    public float jumpButtonGracePeriod;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private float ySpeed;
    private float inputH;
    private float inputV;
    private float originalStepOffset;

    private bool isJumping;
    private bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        originalStepOffset = controller.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        // get input from user
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
        anim.SetBool("isrunning", Input.GetButton("Horizontal") != false);

        if (inputH > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (inputH < 0)
        {
            transform.localScale = new Vector3(1, 1, -1);
        }

        // move player
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * moveSpeed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded) {
            lastGroundedTime = Time.time;
        }

        if ((Input.GetButtonDown("Jump") || (Input.GetButtonDown("Vertical") && inputV > 0))) {
            jumpButtonPressedTime = Time.time;
        }

        // jump
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod) {
            controller.stepOffset = originalStepOffset;
            ySpeed = -0.1f;
            anim.SetBool("isgrounded", true);
            isGrounded = true;
            anim.SetBool("isjumping", false);
            isJumping = false;
            anim.SetBool("isfalling", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod) {
                ySpeed = jumpSpeed;
                anim.SetBool("isjumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        } else {
            controller.stepOffset = 0f;
            anim.SetBool("isgrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2) {
                anim.SetBool("isfalling", true);
            }
        }


        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;
        controller.Move(velocity * Time.deltaTime);
        
    }


}
