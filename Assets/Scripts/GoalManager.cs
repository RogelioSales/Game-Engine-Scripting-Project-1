using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GoalManager 
{
    public Color m_PlayerColor;

    [HideInInspector]
    public int playerNumber;
    [HideInInspector]
    public string coloredPlayerText;
    [HideInInspector]
    public GameObject playerInstance;

    // Use this for initialization
    void Start ()
    {
        coloredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + playerNumber + "</color>";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
