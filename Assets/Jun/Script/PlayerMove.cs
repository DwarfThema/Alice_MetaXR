using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;

    public float speed = 100f;

    public float gravity = -9.18f;
    public float jumpPower = 1;
    float jumpVelocity;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir.Normalize();

        if (controller.isGrounded)
        {
            jumpVelocity = 0;
        }

        jumpVelocity += gravity * time;

        if (Input.GetButtonDown("Jump"))
        {
            jumpVelocity = jumpPower;
        }

        

        dir.y = jumpVelocity;

        controller.Move(dir * speed * time);   
    }
}
