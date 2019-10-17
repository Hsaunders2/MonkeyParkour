using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 10f;
    private CharacterController cc;
    private float verticalVelocity;
    private float gravity = 28.0f;
    private float jumpForce = 10.0f;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float s = 1;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
        {
            s = 3;
        }

        Vector3 forward = transform.forward * v * speed * Time.deltaTime * s;
        Vector3 right = transform.right * h * speed * Time.deltaTime;

        

        if (cc.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        cc.Move(forward + right);
        cc.Move(moveVector * Time.deltaTime);
    }
}
