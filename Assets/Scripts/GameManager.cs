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
    private GoalPost roundWinner;        
    private GoalPost gameWinner;
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
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        SpawnAllTanks();
        SetCameraTargets();

        //StartCoroutine(GameLoop());
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
        // Clear the winner from the previous round.
        roundWinner = null;
        // See if there is a winner now the round is over.
        roundWinner = GetRoundWinner();
        // If there is a winner, increment their score.
        if (roundWinner != null)
            roundWinner.wins++;
        // Now the winner's score has been incremented, see if someone has won the game.
        gameWinner = GetGameWinner();
        // Get a message based on the scores and whether or not there is a game winner and display it.
        string message = EndMessage();
        messageText.text = message;
        // Wait for the specified length of time until yielding control back to the game loop.
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
            return goalPost.ScoreRed >= 1;
        }
        else if(goalPost.IsGoal ==true && goalPost.goalNumber == 2)
        {
            goalPost.ScoreBlue++;
            Destroy(ball);
            return goalPost.ScoreBlue >= 1;
        }
        return false;
    }
    private GoalPost GetRoundWinner()
    {
        // Go through all the tanks...
        if (goalPost.ScoreRed == 1)
        {

        }
        else if (goalPost.ScoreBlue == 1)
        {

        }
        // If none of the tanks are active it is a draw so return null.
        return null;
    }
    private GoalPost GetGameWinner()
    {
        // Go through all the tanks...
        for (int i = 0; i < players.Length; i++)
        {
            // ... and if one of them has enough rounds to win the game, return it.
            if (players[i].wins == numRoundsToWin)
                return players[i];
        }
        // If no tanks have enough rounds to win, return null.
        return null;
    }
    private string EndMessage()
    {
        // By default when a round ends there are no winners so the default end message is a draw.
        string message = "DRAW!";
        // If there is a winner then change the message to reflect that.
        if (roundWinner != null)
            message = roundWinner.coloredPlayerText + " WINS THE ROUND!";
        // Add some line breaks after the initial message.
        message += "\n\n\n\n";
        // Go through all the tanks and add each of their scores to the message.
        for (int i = 0; i < players.Length; i++)
        {
            message += players[i].coloredPlayerText + ": " + players[i].wins + " WINS\n";
        }
        // If there is a game winner, change the entire message to reflect that.
        if (gameWinner != null)
            message = gameWinner.coloredPlayerText + " WINS THE GAME!";

        return message;
    }
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
