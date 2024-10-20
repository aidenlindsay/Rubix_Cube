﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public class TimerDisplay : MonoBehaviour
{
    public Text timer;
    public Text activeText;
    public bool clockActive = false;
    public float timeAmount;
    public bool coroutineRunning = false;

    void Start()
    {
        activeText = GameObject.Find("Canvas/ReadyText").GetComponent<Text>();
        timeAmount = 0;
    }

    void Update()
    {
        if (clockActive == true)
        {
            //increases the time variable with respect to actual time
            timeAmount += Time.deltaTime; 
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeAmount);
        timer.text = timeSpan.Minutes.ToString() +":"+
            timeSpan.Seconds.ToString() + ":" + timeSpan.Milliseconds.ToString();

    }

    public void StartClock()
    {
        clockActive = true;
        coroutineRunning = false;
    }

    public void StopClock()
    {
        clockActive = false;
        if (!coroutineRunning)
        {
            StartCoroutine(Score());
        }
    }

    public void HighscoreText()
    {
        string path = Application.dataPath + "/Highscore.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "1000,1,1");
        }

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeAmount);
        string content = timeSpan.Minutes.ToString("D2") + "," +
                         timeSpan.Seconds.ToString("D2") + "," +
                         timeSpan.Milliseconds.ToString("D3");

        string fileLines = File.ReadAllText(path);
        string[] fileTotal = fileLines.Split(',');

        // Convert to total milliseconds for comparison
        int previousTime = (int.Parse(fileTotal[0]) * 60000) +
                           (int.Parse(fileTotal[1]) * 1000) +
                           int.Parse(fileTotal[2]);

        int newTime = (timeSpan.Minutes * 60000) +
                      (timeSpan.Seconds * 1000) +
                      timeSpan.Milliseconds;

        // Update the high score if the new time is better
        if (newTime <= previousTime)
        {
            File.WriteAllText(path, content);
        }
    }

    public IEnumerator Score()
    {
        coroutineRunning = true;
        activeText.text = "CONGRATS";
        yield return new WaitForSeconds(1);
        activeText.text = "YOUR TIME:";
        yield return new WaitForSeconds(1);
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeAmount);
        activeText.text = timeSpan.Minutes.ToString() + ":" +
            timeSpan.Seconds.ToString() + ":" + timeSpan.Milliseconds.ToString();
        yield return new WaitForSeconds(0.5f);
        activeText.text = "";
        yield return new WaitForSeconds(0.2f);
        activeText.text = timeSpan.Minutes.ToString() + ":" +
            timeSpan.Seconds.ToString() + ":" + timeSpan.Milliseconds.ToString();
        yield return new WaitForSeconds(0.5f);
        activeText.text = "";
        yield return new WaitForSeconds(0.2f);
        activeText.text = timeSpan.Minutes.ToString() + ":" +
            timeSpan.Seconds.ToString() + ":" + timeSpan.Milliseconds.ToString();
        yield return new WaitForSeconds(0.5f);
        activeText.text = "";
        HighscoreText();
        yield return new WaitForSeconds(0.5f);
        timeAmount = 0;
    }

}
