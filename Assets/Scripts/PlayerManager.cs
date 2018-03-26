using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerManager
{

    public Color m_PlayerColor;                             
    public Transform m_SpawnPoint;     
                         
    [HideInInspector]
    public int playerNumber;            
    [HideInInspector]
    public string coloredPlayerText;    
    [HideInInspector]
    public GameObject playerInstance;         
  

    private PlayerMovement playerMovement;                        
    private PlayerShoot playerShooting;
    private PlayerPickUp playerPick;                   
    private GameObject m_CanvasGameObject;

    public void Setup()
    {
        // Get references to the components.
        playerMovement = playerInstance.GetComponent<PlayerMovement>();
        playerShooting = playerInstance.GetComponent<PlayerShoot>();
        playerPick = playerInstance.GetComponent<PlayerPickUp>();
        m_CanvasGameObject = playerInstance.GetComponentInChildren<Canvas>().gameObject;

        // Set the player numbers to be consistent across the scripts.
        playerMovement.playernumber = playerNumber;
        playerShooting.playerNumber = playerNumber;
        playerPick.playerNumber = playerNumber;

        // Create a string using the correct color that says 'PLAYER 1' etc based on the tank's color and the player's number.
        coloredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + playerNumber + "</color>";

        // Get all of the renderers of the tank.
        MeshRenderer[] renderers = playerInstance.GetComponentsInChildren<MeshRenderer>();

        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = m_PlayerColor;
        }
    }
    public void DisableControl()
    {
        playerMovement.enabled = false;
        playerShooting.enabled = false;
        playerPick.enabled = false;

        m_CanvasGameObject.SetActive(false);
    }
    public void EnableControl()
    {
        playerMovement.enabled = true;
        playerShooting.enabled = true;
        playerPick.enabled = true;

        m_CanvasGameObject.SetActive(true);
    }
    public void Reset()
    {
        playerInstance.transform.position = m_SpawnPoint.position;
        playerInstance.transform.rotation = m_SpawnPoint.rotation;

        playerInstance.SetActive(false);
        playerInstance.SetActive(true);
    }
}
