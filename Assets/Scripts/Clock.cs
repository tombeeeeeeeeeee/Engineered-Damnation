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

    private int hours;
    private int minutes;

    [SerializeField] TextMeshProUGUI clockDisplay;

    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        breakTimesIndex %= breakTimes.Length;  

        //Calculate the hours and minutes for the clock
        hours = (int)(Time.timeSinceLevelLoad / (gameLengthInMinutes * 60) * 8) + (militaryTime ? 9 : 8);
        hours = militaryTime ? hours%24 : hours%12 + 1;
        minutes = (int)(Time.timeSinceLevelLoad / (gameLengthInMinutes * 60) * 8 * 60);
        minutes %= 60;

        //change the clocks text
        clockDisplay.text = hours < 10 ? "0" + hours.ToString() : hours.ToString();
        clockDisplay.text += ":" + (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString());

        //check if a break is beginning
        if (breakTimes[breakTimesIndex].x == hours && breakTimes[breakTimesIndex].y == minutes)
        {
            manager.OnBreak = !manager.OnBreak;
            breakTimesIndex++;
        }


    }

    public float playthroughPercentage
    {
        get { return Time.timeSinceLevelLoad / (gameLengthInMinutes * 60); }
    }
}