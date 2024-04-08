using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : PlayerScript
{

    [Header("Player Movement")]

    public float speed;
    
    // Jump
    public float jumpHeight = 3f;
    public float minLongJumpTime;
    public float maxLongJumpTime;
    private float timeJumpPressed = 0;
    private bool releaseJump = false;

    public float gravity = -9.81f;
    public float airMultiplier;
    public float waterMultiplier;

    //private bool readyToJump;

    private float horizontalInput;
    private float verticalInput;

    //private bool jumpPressed;
    

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
        //Debug.Log(timeJumpPressed);
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
            //Faire en sorte de bloquer les déplacement quand le joueur appuis sur espace (revoir l'archi du script)
            timeJumpPressed += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            releaseJump = true;
        }

    }

    private void MovePlayer()
    {

        if(releaseJump && grounded)
        {
            Jump();
            releaseJump = false;
            timeJumpPressed = 0;
        }

        /*if(readyToJump && grounded && jumpPressed)
        {
            readyToJump = false;
            Jump();
        }*/

        if(timeJumpPressed == 0)
        {
            Deplacement();
        }

        if (grounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if(velocity.x > 0.1f || velocity.x < -0.1f)
        {
            velocity.x *= 0.93f;
        }
        else
        {
            velocity.x = 0f;
        }
        
        velocity.y += gravity * Time.deltaTime;

        if (velocity.z > 0.1f || velocity.z < -0.1f)
        {
            velocity.z *= 0.93f;
        }
        else
        {
            velocity.z = 0f;
        }

        controller.Move(velocity * Time.deltaTime);
    
    }

    private void Deplacement()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.y = 0;

        if (!grounded)
        {
            controller.Move(moveDirection.normalized * speed * airMultiplier * Time.deltaTime);
        }
        else
        {
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }

    private void MovePlayerWater()
    {
        if (timeJumpPressed!=0)
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

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        //Debug.Log(orientation.forward);
        //moveDirection = orientation.forward * verticalInput;

        controller.Move(moveDirection.normalized * speed * waterMultiplier * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

    }

    private void Jump()
    {
        velocity = orientation.forward.normalized * 3;
        velocity.y = Mathf.Sqrt(jumpHeight * -gravity);

        controller.Move(velocity * Time.deltaTime);
        // Faire en sorte de prendre la direction pour mettre une impulsion dans cette direction
    }

    private void JumpWater()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * 1f / 2);
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
