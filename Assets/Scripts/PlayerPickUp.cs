using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField]
    private GameObject item;
    [SerializeField]
    private Transform hand;
    [SerializeField]
    private GameObject player;     
    [SerializeField]
    private float range;


    private bool isCarrying;
   private bool isInRange;
  
    private void Start()
    {
        item.GetComponent<Rigidbody>().useGravity = true;
       
    }
    private void Update()
    {

        if (Physics.Raycast(player.transform.position,Vector3.forward, range))
        {
           if (Input.GetButtonDown("PickUp"))

            {
                Pickup();
                isCarrying = true;
            }
           
        }
        else if (isCarrying == true)
        {
            if (Input.GetButtonDown("PickUp"))
            {
                Drop();
                isCarrying = false;
            }
        }
    }
    private void Pickup()
    {
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.position = hand.transform.position;
        item.transform.rotation = hand.transform.rotation;
        player.GetComponent<Rigidbody>().isKinematic = true;  
        item.transform.SetParent(hand.transform);
    }
    private void Drop()
    {
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.SetParent(null);
        item.transform.position = hand.transform.position;
        player.GetComponent<Rigidbody>().isKinematic = false;
    }

}
