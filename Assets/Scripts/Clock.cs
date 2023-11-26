using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public bool militaryTime;
    [SerializeField] Vector2[] breakTimes;
    [SerializeField] SystemManager manager;
    private int breakTimesIndex = 0;
    public float gameLengthInMinutes;

    [SerializeField] int startingTime;
    [SerializeField] int shiftLength;
    [SerializeField] SequenceObject endingSequenceStarter;

    private float hours;
    private float minutes;

    private bool gameFinished = false;

    [SerializeField] TextMeshProUGUI clockDisplay;

    private void Start()
    {
        Gameplay.timeSinceStart = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Gameplay.deltaTime = Time.deltaTime * (Gameplay.gameplayActive ? 1 : 0);
        Gameplay.timeSinceStart += Gameplay.deltaTime;

        breakTimesIndex %= breakTimes.Length;  

        //Calculate the hours and minutes for the clock
        hours = playthroughPercentage * shiftLength + (militaryTime ? startingTime : startingTime - 1);
        hours = militaryTime ? hours % 24 : hours % 12 + 1;
        minutes = (int)(Gameplay.timeSinceStart / gameLengthInMinutes * shiftLength);
        minutes %= 60;

        //change the clocks text
        clockDisplay.text = hours < 10 ? "0" + ((int)hours).ToString() : ((int)hours).ToString();
        clockDisplay.text += ":" + (minutes < 10 ? "0" + ((int)minutes).ToString() : ((int)minutes).ToString());


        //check if a break is beginning
        if (breakTimes[breakTimesIndex].x == hours && breakTimes[breakTimesIndex].y == minutes)
        {
            manager.OnBreak = !manager.OnBreak;
            breakTimesIndex++;
        }

        else if(playthroughPercentage >= 1 && !gameFinished)
        {
            gameFinished = true;
            endingSequenceStarter.Begin(
                manager.winState);
        }

    }


    public float playthroughPercentage
    {
        get { return Gameplay.timeSinceStart / (gameLengthInMinutes * 60); }
    }


}