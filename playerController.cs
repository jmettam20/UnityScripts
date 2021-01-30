using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public CharacterController controller; //created controller

    public float speed; //player speed

    public float turnSmoothTime = 0.01f;

    float turnSmoothVelocity;

    public Transform cam;


    public float runSpeed = 6f;

    public float walkSpeed = 4f;

    Animator anim;

    public LayerMask groundLayers;

    public float jumpForce = 7;


    public SphereCollider col;

    private Rigidbody rigbod;


    void Start()
    {

        anim = GetComponent<Animator>();
        rigbod = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }


    ///CHECK_IF_GROUNDED
    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);

    }
    /// ////////////


    // Update is called once per frame
    void Update()
    {

        //JUMP_CHECK
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rigbod.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);


        }
        ////////////////


        ///WALKING///
        float horizontal = Input.GetAxisRaw("Horizontal"); //get horizontal axis
        float vertical = Input.GetAxisRaw("Vertical"); //get vertical axis

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; //keeps track of players direction y is 0 because we wont be going up , normalizeing prevents unwanted increase of speed when moving diagonally 




        ///SPRINTING//////

        if (direction.magnitude >= 0.1f)
        { // check if moving 

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
                anim.SetBool("Sprinting", true);

            }
            else
            {
                speed = walkSpeed;
                anim.SetBool("Sprinting", false);



            }

            //walking movement
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; //returns angle between x axis and 0 so it says what angle the player is looking , converts angle to degrees
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime); //moving the character, *Time.deltaTime makes movement framerate independant 


        }
        ////////////////////////////////////


    }

}
