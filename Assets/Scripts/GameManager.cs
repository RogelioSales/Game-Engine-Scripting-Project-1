using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numRoundsToWin = 5;          
    public float startDelay = 3f;            
    public float endDelay = 3f;              
    public CameraControl cameraControl;      
    public Text messageText;                
    public GameObject playerPrefab;
    public GameObject ballPrefab;           
    public PlayerManager[] players;
    public Transform ballSpawnPoint;

    private int roundNumber;                  
    private WaitForSeconds startWait;         
    private WaitForSeconds endWait;          
           
    private GoalPost gameWinner;
    private int teamRd;
    private int teamBlue;
    [HideInInspector]
    public GameObject Ball
    {
        get { return ball; }
        set { ball = value; }
    }
    private GameObject ball;
    private GoalPost goalPost;

    private void Start()
    {
        goalPost = GameObject.FindGameObjectWithTag("Goals").GetComponent<GoalPost>();
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        SpawnAllTanks();
        SetCameraTargets();
        StartCoroutine(GameLoop());
    }
    private void SpawnAllTanks()
    {
       ball = Instantiate(ballPrefab, ballSpawnPoint.position, ballSpawnPoint.rotation) as GameObject;

        for (int i = 0; i < players.Length; i++)
        {
            // ... create them, set their player number and references needed for control.
            players[i].playerInstance =
                Instantiate(playerPrefab, players[i].m_SpawnPoint.position, players[i].m_SpawnPoint.rotation) as GameObject;
            players[i].playerNumber = i + 1;
            players[i].Setup();
        }
    }
    private void SetCameraTargets()
    {
        // Create a collection of transforms the same size as the number of tanks.
        Transform[] targets = new Transform[players.Length];
        // For each of these transforms...
        for (int i = 0; i < targets.Length; i++)
        {
            // ... set it to the appropriate tank transform.
            targets[i] = players[i].playerInstance.transform;
        }
        // These are the targets the camera should follow.
        cameraControl.m_Targets = targets;
    }
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());
        if (gameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }
    private IEnumerator RoundStarting()
    {
        ball = Instantiate(ballPrefab, ballSpawnPoint.position, ballSpawnPoint.rotation) as GameObject;
        ResetAllPlayer();
        goalPost.Reset();
        DisablePlayerControl();

        cameraControl.SetStartPositionAndSize();
        roundNumber++;
        messageText.text = "ROUND " + roundNumber;

        yield return startWait;
    }
    private IEnumerator RoundPlaying()
    {
        EnablePlayerControl();
        messageText.text = string.Empty;
        while (!GoalShot())
        {
            yield return null;
        }
    }
    private IEnumerator RoundEnding()
    {
        DisablePlayerControl();
        GetRoundWinner();
        GetGameWinner();       
        yield return endWait;
    }
    private bool GoalShot()
    {
        goalPost.ScoreRed = 0;
        goalPost.ScoreBlue = 0;
        if (goalPost.IsGoal == true && goalPost.goalNumber == 1)
        {
            goalPost.ScoreRed++;
            Destroy(ball);
            goalPost.ScoreRed = teamRd;
            return teamRd >= 1;
        }
        else if(goalPost.IsGoal ==true && goalPost.goalNumber == 2)
        {
            goalPost.ScoreBlue++;
            Destroy(ball);
            goalPost.ScoreBlue = teamBlue;
            return teamBlue >= 1;
        }
        return false;
    }
    private void GetRoundWinner()
    {
        Debug.Log(teamRd);
        Debug.Log(teamBlue);
        if (teamRd == 1)
        {
            goalPost.winsRed++;
        }
        else if (teamBlue == 1)
        {
            goalPost.wins++;
        }     
    }
    private void GetGameWinner()
    {
        if (goalPost.wins == numRoundsToWin)
        {
           
        }
        else if (goalPost.winsRed == numRoundsToWin)
        {
           
        }
    }
    //private string EndMessage()
    //{
    //}
    private void ResetAllPlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
           players[i].Reset();
        }
    }
    private void EnablePlayerControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
        }
    }
    private void DisablePlayerControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DisableControl();
        }
    }
}
