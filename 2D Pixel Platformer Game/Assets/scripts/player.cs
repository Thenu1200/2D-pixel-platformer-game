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
    private float ySpeed;
    private float inputH;
    private float inputV;
    private float originalStepOffset;

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

        // jump
        if (controller.isGrounded) {
            controller.stepOffset = originalStepOffset;
            ySpeed = -0.1f;
            if (Input.GetButton("Jump") || (Input.GetButton("Vertical") && inputV > 0)) {
                ySpeed = jumpSpeed;
            }
        } else {
            controller.stepOffset = 0f;
        }


        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;
        controller.Move(velocity * Time.deltaTime);
        
    }


}
