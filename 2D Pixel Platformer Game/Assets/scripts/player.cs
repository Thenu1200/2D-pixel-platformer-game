using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour


{
    public Animator anim;
    public Rigidbody rbody;
    // Start is called before the first frame update

    private float inputH;
    private float inputV;

    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
        anim.SetBool("isrunning", Input.GetButton("Horizontal") != false);

        float direction = 1f;

        //if sign changes in inputH, flip the character
        if (inputH > 0)
        {
            transform.localScale = Vector3.one;
            direction = 1f;
        }
        else if (inputH < 0)
        {
            transform.localScale = new Vector3(1, 1, -1);
            direction = -1f;
        }

        float isMoving = 0;
        if (Input.GetButton("Horizontal"))
        {
            isMoving = 1;
        }
        else
        {
            isMoving = 0;
        }

        rbody.velocity = new Vector2(isMoving * 10f * direction, rbody.velocity.y);
    }


}
