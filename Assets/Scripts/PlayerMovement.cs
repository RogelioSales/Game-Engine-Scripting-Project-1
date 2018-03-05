using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Unity Editor Display
    [SerializeField]
    private int number = 1;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float turnSpeed = 180f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float JumpFall = -5f;
   

    // No Unity Editor Display
    private Rigidbody p_Rigidbody;
    private string moveAxisN;
    private string turnAxisN;
    private string jumpAxisN;
    private float moveValue;
    private float turnValue;
    private float jumpValue;
    private bool onGround;

    private void Awake()
    {
        p_Rigidbody = GetComponent<Rigidbody>();
    }
	private void Start ()
    {
        onGround = true;
        moveAxisN = "Vertical" + number;
        turnAxisN = "Horizontal" + number;
        jumpAxisN = "Jump" + number;
	}
	private void Update ()
    {
        moveValue = Input.GetAxis(moveAxisN);
        turnValue = Input.GetAxis(turnAxisN);
        jumpValue = Input.GetAxis(jumpAxisN);
	}
    private void FixedUpdate()
    {
        Move();
        Turn();
        Jump();
    }
    private void Move()
    {
        Vector3 movement = 
            transform.forward * moveValue * speed * Time.deltaTime;

        p_Rigidbody.MovePosition(p_Rigidbody.position + movement);
    }
    private void Turn()
    {
        float turn = turnValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        p_Rigidbody.MoveRotation(p_Rigidbody.rotation * turnRotation);
    }
    private void Jump()
    {
        Vector3 jumpdown =
            transform.up * JumpFall * Time.captureFramerate;

        if (onGround)
        {
            if(jumpValue > 0.5f)
            {
                p_Rigidbody.velocity = new Vector3(0f, jumpForce, 0f); 
                onGround = false;
               
                
            }
        }
        if (onGround == false)
        {
            p_Rigidbody.AddForce(jumpdown, ForceMode.Force);
           
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
       
    }
}
