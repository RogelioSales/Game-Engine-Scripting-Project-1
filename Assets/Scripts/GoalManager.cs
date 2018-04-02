using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GoalManager 
{
    public Color goalColor;
    public Transform goalSpawnPoint;
    [HideInInspector]
    public int goalNumber;
    [HideInInspector]
    public string coloredgoalText;
    [HideInInspector]
    public GameObject goalInstance;
    [HideInInspector]
    public int wins;

    private GoalPost goalPost;
    private GameObject canvasGameobject;

    public void Setup()
    {
        goalPost = goalInstance.GetComponent<GoalPost>();
        canvasGameobject = goalInstance.GetComponentInChildren<Canvas>().gameObject;
        goalPost.goalNumber = goalNumber;
        coloredgoalText = "<color=#" + ColorUtility.ToHtmlStringRGB(goalColor) + "> TEAM " + goalNumber + "</color>";

        MeshRenderer[] renderers = goalInstance.GetComponentsInChildren<MeshRenderer>();

        for(int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = goalColor;
        }
    }
    public void DisableControl()
    {
        goalPost.enabled = false;
        canvasGameobject.SetActive(false);
    }
    public void EnableControl()
    {
        goalPost.enabled = true;
        canvasGameobject.SetActive(true);
    }
    public void Reset()
    {
        goalInstance.transform.position = goalSpawnPoint.position;
        goalInstance.transform.rotation = goalSpawnPoint.rotation;
        goalInstance.SetActive(false);
        goalInstance.SetActive(true);
    }
}
