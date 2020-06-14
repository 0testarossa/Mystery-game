using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    float speed = 10;
    float gravity = 12;
    float jumpSpeed = 5;

    //Przechowuje pozycję gracza z ostatniej klatki gry
    private Vector3 lastPosition;
    //Wysokość poniżej której nie można się znaleźć
    private float minimumHeight = -1.0f; 

    private Vector3 moveDir = Vector3.zero;
    private float lastUpdatePositionDelta;
    private const float updatePositionDelta = 0.2f;

    private bool isGrounded;

    public Animator anim;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        lastUpdatePositionDelta = 0;
        isGrounded = true;
        controller = GetComponentInChildren<CharacterController>();
        updateLastPosition();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        updateLastPosition();
    }

    //Ustawia ostatnią pozycję na aktualną
    void updateLastPosition()
    {
        lastUpdatePositionDelta += Time.deltaTime;
        if(lastUpdatePositionDelta >= updatePositionDelta)
        {
            if (controller.transform.position.y > minimumHeight+0.4f && isGrounded)
            {
                lastPosition = new Vector3(controller.transform.position.x,
                controller.transform.position.y,
                controller.transform.position.z);
                lastUpdatePositionDelta = 0;
            } 
        }
    }

    void Movement()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -150 * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(0, 150 * Time.deltaTime, 0);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            transform.Rotate(0, 0, 0);
        }


            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isRunning", true);
                moveDir = Vector3.forward;
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
                
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isRunning", true);
                moveDir = new Vector3(0, 0, -1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }


        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            moveDir = new Vector3(0, 0, 0);
        }

        
        moveDir.y -= gravity * Time.deltaTime;
        //Jeżeli jesteśmy zbyt nisko to cofnij nas do ostatniej pozcji (np. wpadliśmy do jeziora)
        //Zapobiega dostaniu się do jeziora
        if (transform.position.y < minimumHeight)
        {
            controller.enabled = false;
            controller.transform.position = lastPosition;
            controller.enabled = true;
            controller.SimpleMove(Vector3.zero);

        }
        
        controller.Move(moveDir * Time.deltaTime);


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
            if (!isGrounded) isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
            if (isGrounded) isGrounded = false;
    }

}
