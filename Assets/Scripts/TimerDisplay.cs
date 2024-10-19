using System.Collections;
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
    }

    public void StopClock()
    {
        clockActive = false;
        if (coroutineRunning == false)
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
        string content = timeSpan.Minutes.ToString()
            + "," + timeSpan.Seconds.ToString()
            + "," + timeSpan.Milliseconds.ToString();

        string fileLines = File.ReadAllText(path);
        string[] fileTotal = fileLines.Split(",".ToCharArray());

        //compares the two times by turning the values into integers that can be compared
        if((int.Parse(fileTotal[0])* 60000) + (int.Parse(fileTotal[1])*1000) + int.Parse(fileTotal[2])
            >= (timeSpan.Minutes*60000) + (timeSpan.Seconds*1000) + timeSpan.Milliseconds)
        {
            File.WriteAllText(path, content);
        }
        var info = new FileInfo(path);
        if (info.Length == 0)
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
