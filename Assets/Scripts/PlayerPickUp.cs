using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public int playerNumber = 1;
    public GameObject item;
    [SerializeField]
    private GameObject tempParent;
    [SerializeField]
    private Transform hand;
    private GameObject ball;

    [HideInInspector]
    public bool IsCarrying
    {
        get { return isCarrying; }
        set { isCarrying = value; }
    }
    bool isCarrying;

    private PlayerShoot playerShoot;
    private GameManager game;
    private string pickable;
    

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameManager>();
        game.Ball.GetComponent<Rigidbody>().useGravity = true;
        pickable = "PickUp" + playerNumber;
        
    }
    private void OnTriggerStay(Collider other)
    {
       
        if (isCarrying == false && this.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered");
            if (Input.GetButtonDown(pickable))
            {
                pickup();
                isCarrying = true;
            }
        }
        else if (isCarrying == true  )
        {
            Debug.Log("being carried");
            if (Input.GetButtonDown(pickable))
            {
                drop();
                isCarrying = false;
            }
        }
    }
    private void pickup()
    {
        game.Ball.GetComponent<Rigidbody>().useGravity = false;
        game.Ball.GetComponent<Rigidbody>().isKinematic = true;
        game.Ball.transform.position = hand.transform.position;
        game.Ball.transform.rotation = hand.transform.rotation;
        game.Ball.transform.parent = tempParent.transform;
    }
    private void drop()
    {
        game.Ball.GetComponent<Rigidbody>().useGravity = true;
        game.Ball.GetComponent<Rigidbody>().isKinematic = false;
        game.Ball.transform.parent = null;
        game.Ball.transform.position = hand.transform.position;
    }

}
