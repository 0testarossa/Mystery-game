﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPlotLogic : MonoBehaviour
{
    public string correctNPC = "orange";
    private string[] correctNPCs = { "orange", "green", "blue", "yellow", "finished" };
    private int correctNPCIndex;
    // Start is called before the first frame update
    void Start()
    {
        correctNPCIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCorrectNPC()
    {
        if(correctNPC != "finished")
        {
            correctNPC = correctNPCs[++correctNPCIndex];
        }
    }
}
