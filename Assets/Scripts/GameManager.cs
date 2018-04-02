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
    public GameObject goalPrefab;
    public PlayerManager[] players;
    public GoalManager[] goalsPost;
    public Transform ballSpawnPoint;

    private int roundNumber;                  
    private WaitForSeconds startWait;         
    private WaitForSeconds endWait;
    private GoalManager roundWinner;       
    private GoalManager gameWinner;
    [HideInInspector]
    public GameObject Ball
    {
        get { return ball; }
        set { ball = value; }
    }
    private GameObject ball;

    private void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        SpawnAllPrefabs();
        SetCameraTargets();
        StartCoroutine(GameLoop());
    }
    private void SpawnAllPrefabs()
    {
        ball = Instantiate(ballPrefab, ballSpawnPoint.position, ballSpawnPoint.rotation) as GameObject;

        for(int g = 0; g < goalsPost.Length; g++)
        {
            goalsPost[g].goalInstance =
                Instantiate(goalPrefab, goalsPost[g].goalSpawnPoint.position, goalsPost[g].goalSpawnPoint.rotation) as GameObject;
            goalsPost[g].goalNumber = g + 1;
            goalsPost[g].Setup();
        }
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
        
        ResetAllPlayer();
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
        roundWinner = null;
        roundWinner = GetRoundWinner();
        if (roundWinner != null)
            roundWinner.wins++;
        gameWinner = GetGameWinner();
        string message = EndMessage();
        messageText.text = message;

        yield return endWait;
    }
    private bool GoalShot()
    {
        int numgoals = 0;

        for (int i = 0; i <goalsPost.Length;i++)
        {
            if (goalsPost[i].goalInstance.activeSelf)
                numgoals++;
        }
        return numgoals <= 1;
      
    }
    private GoalManager GetRoundWinner()
    {
        for (int i = 0; i< goalsPost.Length;i++)
        {
            if (goalsPost[i].goalInstance.activeSelf)
            {
                return goalsPost[i];
            }
        }
        return null;
    }
    private GoalManager GetGameWinner()
    {
        for(int i = 0; i < goalsPost.Length; i++)
        {
            if (goalsPost[i].wins == numRoundsToWin)
            {
                return goalsPost[i];
            }
        }
        return null;
    }
    private string EndMessage()
    {
        string message = "Draw";
        if (roundWinner != null)
            message = roundWinner.coloredgoalText + "Wins The Round!";

        message += "\n\n\n\n\n";
        for (int i = 0; i < goalsPost.Length; i++)
        {
            message += goalsPost[i].coloredgoalText + ": " + goalsPost[i].wins + "Wins\n";
        }
        if (gameWinner != null)
            message = gameWinner.coloredgoalText + "WINS THE GAME!!";
        return message;
    }
    private void ResetAllPlayer()
    {
        ball.transform.position = ballSpawnPoint.position;
        ball.transform.rotation = ballSpawnPoint.rotation;
        ball.SetActive(false);
        ball.SetActive(true);
        for (int i = 0; i < players.Length; i++)
        {
           players[i].Reset();
        }
        for(int g =0; g < goalsPost.Length; g++)
        {
            goalsPost[g].Reset();
        }
    }
    private void EnablePlayerControl()
    {
        ball.GetComponent<Rigidbody>().isKinematic = false;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
        }
        for (int g =0; g< goalsPost.Length;g++)
        {
            goalsPost[g].EnableControl();
        }
    }
    private void DisablePlayerControl()
    {
        ball.GetComponent<Rigidbody>().isKinematic = true;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DisableControl();
        }
        for (int g= 0; g<goalsPost.Length; g++)
        {
            goalsPost[g].DisableControl();
        }
    }
}
