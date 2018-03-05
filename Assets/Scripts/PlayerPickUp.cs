using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField]
    private GameObject item;
    [SerializeField]
    private GameObject tempParent;
    [SerializeField]
    private Transform hand;


    private bool isCarrying;
  
    private void Start()
    {
        item.GetComponent<Rigidbody>().useGravity = true;
    }
    private void Update()
    {
        if (isCarrying == false)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                pickup();
                isCarrying = true;
            }
        }
        else if (isCarrying == true)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                drop();
                isCarrying = false;
            }
        }
    }
    private void pickup()
    {
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.position = hand.transform.position;
        item.transform.rotation = hand.transform.rotation;
        item.transform.parent = tempParent.transform;
    }
    private void drop()
    {
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        item.transform.position = hand.transform.position;
    }

}
