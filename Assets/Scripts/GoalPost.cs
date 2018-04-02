using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    public int goalNumber;
    [HideInInspector]
    public bool IsGoal
    {
        get { return isGoal; }
        set { isGoal = value; }
    }
    private bool isGoal;
    private GameManager game;
    [HideInInspector]
    public int wins;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickable")
        {
            Debug.Log("GOAALLL");
            this.gameObject.SetActive(false);
            isGoal = true;
        }
    }
    // Use this for initialization
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameManager>();
        isGoal = false;
    }
}


