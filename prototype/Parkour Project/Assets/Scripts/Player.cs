using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private Transform grappleRope;

    public float speed = 10f;
    private CharacterController cc;
    private float verticalVelocity;
    private float gravity = 28.0f;
    private float jumpForce = 10.0f;

    private bool crouch = false;
    private float ogHeight;
    private float crouchHeight = 0.3f;
    GameObject camera1;
    RaycastHit raycastHit;
    Vector3 grappleGunPosition;
    private State state;
    float timer = 4;
    float grappleRopeSize;

    private enum State
    {
        Normal,
        grappleShot,
        grappleGunMoving,
        slowmo
    }

    // Use this for initialization


    void Start () {
        cc = GetComponent<CharacterController>();
        ogHeight = cc.height;
        Cursor.lockState = CursorLockMode.Locked;
        camera1 = GameObject.FindGameObjectWithTag("boopbeep1");
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                gravityChanger();
                movement();
                break;
            case State.grappleGunMoving:
                gravityChanger();
                grappleGunMovement();
                break;
            case State.slowmo:
                movement();
                gravityChanger();
                break;
            case State.grappleShot:
                movement();
                grappleRopeShot();
                gravityChanger();
                break;
        }
        movement();

        timer += Time.deltaTime;
        if (state == State.slowmo)
        {
            if (timer > 0.75)
            {
                state = State.Normal;
            }
        } 
    }

    void cCrouch()
    {
        if(crouch == true)
        {
            cc.height = crouchHeight;
        }
        else
        {
            cc.height = ogHeight;
        }
    }

    private void grappleGun()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

   
            if (Physics.Raycast(camera1.transform.position, camera1.transform.forward, out raycastHit))
            {
                grappleGunPosition = raycastHit.point;
                grappleRopeSize = 0f;
                state = State.grappleShot;
            }
        }
    }

    private void grappleGunMovement()
    {

        grappleRope.LookAt(grappleGunPosition);
        Vector3 grappleGunDirection = (grappleGunPosition - transform.position).normalized;


        float gunSpeed = 20f;

        cc.Move(grappleGunDirection * gunSpeed * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            timer = 0;
            grappleRope.gameObject.SetActive(false);
            state = State.slowmo;
        }

        float ggEnd = 1.0f;
        if (Vector3.Distance(transform.position, grappleGunPosition) < ggEnd)
        {
            timer = 0;
            grappleRope.gameObject.SetActive(false);
            state = State.slowmo;
        }

    }

    private void movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float s = 1;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
        {
            s = 3;
        }

        if (state == State.slowmo)
        {
            speed = 1.5f;
        }
        else
        {
            speed = 10f;
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

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch = true;

            cCrouch();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouch = false;

            cCrouch();
        }
        if (state == State.Normal)
        {
            grappleGun();
        } else if (state == State.slowmo)
        {
            grappleGun();
        }
        
    }

    private void gravityChanger()
    {
        if (state == State.Normal)
        {
            gravity = 14.0f;
        } else if(state == State.slowmo)
        {
            gravity = 0.5f;
        }
        else
        {
            gravity = 0.1f;
        }
    }

    private void grappleRopeShot()
    {
        grappleRope.gameObject.SetActive(true);
        grappleRope.LookAt(grappleGunPosition);
        grappleRopeSize += 20f * Time.deltaTime;
        grappleRope.localScale = new Vector3(1, 1, grappleRopeSize);


        if (grappleRopeSize >= Vector3.Distance(transform.position, grappleGunPosition))
        {
            state = State.grappleGunMoving;
        }
    }
}
