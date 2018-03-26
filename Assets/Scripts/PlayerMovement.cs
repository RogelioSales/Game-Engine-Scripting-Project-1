using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Unity Editor Display
    public int playernumber = 1;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float turnSpeed = 180f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float jumpFall = -5f;

    // No Unity Editor Display
    private Rigidbody playerRigidbody;
    private string p_MoveAxisN;
    private string p_TurnAxisN;
    private string p_JumpAxisN;
    private float p_MoveValue;
    private float p_TurnValue;
    private float p_JumpValue;
    private bool onGround;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }
	private void Start ()
    {
        onGround = true;
        p_MoveAxisN = "Vertical" + playernumber;
        p_TurnAxisN = "Horizontal" + playernumber;
        p_JumpAxisN = "Jump" + playernumber;
	}
	private void Update ()
    {
        p_MoveValue = Input.GetAxis(p_MoveAxisN);
        p_TurnValue = Input.GetAxis(p_TurnAxisN);
        p_JumpValue = Input.GetAxis(p_JumpAxisN);
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
            transform.forward * p_MoveValue * speed * Time.deltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }
    private void Turn()
    {
        float turn = p_TurnValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
    }
    private void Jump()
    {
        Vector3 jumpdown =
            transform.up * jumpFall * Time.captureFramerate;

        if (onGround)
        {
            if(p_JumpValue > 0.5f)
            {
                playerRigidbody.velocity = new Vector3(0f, jumpForce, 0f); 
                onGround = false;
            }
        }
        if (onGround == false)
        {
            playerRigidbody.AddForce(jumpdown, ForceMode.Force);
           
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
