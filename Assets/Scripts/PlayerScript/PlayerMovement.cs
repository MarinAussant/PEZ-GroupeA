using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : PlayerScript
{

    private float horizontalInput;
    private float verticalInput;
    private bool jumpPressed;

    [Header("Player Variable")]
    public float speed;
    public float jumpHeight = 3f;

    public float gravity = -9.81f;
    public float airMultiplier;

    private bool readyToJump;
    public bool grounded;
    public bool inWater;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [Header("Ground Check")]
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    public Transform groundCheck;

    [Header("Other Object")]
    public Transform orientation;
    public CharacterController controller;
    public CapsuleCollider capsuleCollider;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics.Raycast(transform.position, Vector3.down, 1 * 0.6f, groundMask);
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();

        if (grounded)
        {
            readyToJump = true;
        }
        else
        {
            readyToJump = false;
        }
    }

    private void FixedUpdate()
    {
        if ((inWater))
        {
            MovePlayerWater();
        }
        else
        {
            MovePlayer();
        }
        
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            jumpPressed = true;
        }
        else
        {
            jumpPressed = false;
        }

    }

    private void MovePlayer()
    {

        if(readyToJump && grounded && jumpPressed)
        {
            readyToJump = false;
            Jump();
        }

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (!grounded)
        {
            controller.Move(moveDirection.normalized * speed * airMultiplier * Time.deltaTime);
        }
        else
        {
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    
    }

    private void MovePlayerWater()
    {
        if (jumpPressed)
        {
            JumpWater();
        }
        else if (velocity.y > -0.5)
        {
            velocity.y -= Time.deltaTime;
        }
        else
        {
            velocity.y = -0.5f;
        }

        //Modifier pour que le rat avance dans la direction de la caméra et qu'appuyer sur Space lui donne simplement une impulsion verticale en plus
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (!grounded)
        {
            controller.Move(moveDirection.normalized * speed * airMultiplier * Time.deltaTime);
        }
        else
        {
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        

        controller.Move(velocity * Time.deltaTime);

    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -gravity);
    }

    private void JumpWater()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * 1f);
    }

    public void SetInWater(bool inWater)
    {
        this.inWater = inWater;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            inWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            inWater = false;
        }
    }

}
