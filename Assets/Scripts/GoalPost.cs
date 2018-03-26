using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    public int goalNumber;
    
    [HideInInspector]
    public int ScoreBlue
    {
        get { return scoreBlue; }
        set { scoreBlue = value; }
    }
    [HideInInspector]
    public int ScoreRed
    {
        get { return scoreRed; }
        set { scoreRed = value; }
    }
    [HideInInspector]
    public bool IsGoal
    {
        get { return isGoal; }
        set { isGoal = value; }
    }
    private bool isGoal;
    private int scoreBlue = 0;
    private int scoreRed = 0;
    private GameManager game;
    [HideInInspector]
    public int wins;
    [HideInInspector]
    public int winsRed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickable" && goalNumber == 1)
        {
            Debug.Log("GOAALLL");
            isGoal = true;
            
        }
        else if(other.gameObject.tag == "Pickable" && goalNumber == 2)
        {
            Debug.Log("Goaal");
            isGoal = true;
           
        }

    }
	// Use this for initialization
	void Start ()
    {
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameManager>();
        isGoal = false;
    }
    public void Reset()
    {
        scoreRed = 0;
        scoreBlue = 0;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
