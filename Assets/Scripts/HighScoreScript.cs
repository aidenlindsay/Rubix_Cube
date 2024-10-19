using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HighScoreScript : MonoBehaviour
{
    public Text highScore;
    public string path;
    // Start is called before the first frame update
    void Start()
    {
        highScore = GameObject.Find("Canvas/HighScore").GetComponent<Text>();
        path = Application.dataPath + "/Highscore.txt";
    }

    // Update is called once per frame
    void Update()
    {
        //will fetch from the highscore list, and display the users high score
        //if no high score, there will be nothing displayed
        
        var info = new FileInfo(path);
        /*if (info.Length == 0)
        {
            highScore.text = "Highscore: x:x:x";
        }*/
            string fileLines = File.ReadAllText(path);
            string[] fileTotal = fileLines.Split(",".ToCharArray());
            highScore.text = "Highscore: " + fileTotal[0] + ":" + fileTotal[1] + ":" + fileTotal[2];
        
    }
}
