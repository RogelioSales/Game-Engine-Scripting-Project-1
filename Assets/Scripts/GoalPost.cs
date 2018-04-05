using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    public int goalNumber;
    public GameObject particle1;
    public GameObject particle2;
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
            particle1.gameObject.SetActive(true);
            particle1.GetComponent<ParticleSystem>().Play();
            particle2.gameObject.SetActive(true);
            particle2.GetComponent<ParticleSystem>().Play();
        }
    }
    // Use this for initialization
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameManager>();
        isGoal = false;
        particle1.SetActive(false);
        particle2.SetActive(false);
    }
}


