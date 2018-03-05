using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Unity Editor Display
    [SerializeField]
    private int p_Number = 1;
    [SerializeField]
    private float p_Speed = 10f;
    [SerializeField]
    private float p_TurnSpeed = 180f;
    [SerializeField]
    private float p_JumpForce = 10f;
    [SerializeField]
    private float p_JumpFall = -5f;

    // No Unity Editor Display
    private Rigidbody p_Rigidbody;
    private string p_MoveAxisN;
    private string p_TurnAxisN;
    private string p_JumpAxisN;
    private float p_MoveValue;
    private float p_TurnValue;
    private float p_JumpValue;
    private bool onGround;

    private void Awake()
    {
        p_Rigidbody = GetComponent<Rigidbody>();
    }
	private void Start ()
    {
        onGround = true;
        p_MoveAxisN = "Vertical" + p_Number;
        p_TurnAxisN = "Horizontal" + p_Number;
        p_JumpAxisN = "Jump" + p_Number;
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
            transform.forward * p_MoveValue * p_Speed * Time.deltaTime;

        p_Rigidbody.MovePosition(p_Rigidbody.position + movement);
    }
    private void Turn()
    {
        float turn = p_TurnValue * p_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        p_Rigidbody.MoveRotation(p_Rigidbody.rotation * turnRotation);
    }
    private void Jump()
    {
        Vector3 jumpdown =
            transform.up * p_JumpFall * Time.captureFramerate;

        if (onGround)
        {
            if(p_JumpValue > 0.5f)
            {
                p_Rigidbody.velocity = new Vector3(0f, p_JumpForce, 0f); 
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
