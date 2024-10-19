using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomScript : MonoBehaviour
{
    public bool compFront = false;
    public bool compBack = false;
    public bool compLeft = false;
    public bool compRight = false;
    public bool compUp = false;
    public bool compDown = false;

    public bool userBlock = false;

    public Text readyText;

    public bool scrambled = false;
    public bool scrambleBegun = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && scrambleBegun == false)
        {
            StartCoroutine(Scramble());
        }
    }

    public IEnumerator Scramble()
    {
        scrambleBegun = true;
        //userBlock = true;
        for (int i = 0; i < 16; i++)
        {
            int random = UnityEngine.Random.Range(1, 7);
            //print(random);
            switch (random)
            {
                //each number corresponds to a face that is moved
                //the number from the random generator triggers the move
                case 1:
                    compFront = true;
                    break;
                case 2:
                    compBack = true;
                    break;
                case 3:
                    compLeft = true;
                    break;
                case 4:
                    compRight = true;
                    break;
                case 5:
                    compUp = true;
                    break;
                case 6:
                    compDown = true;
                    break;
            }
            yield return new WaitForSeconds(0.4f);
        }
        readyText.text = ("READY!");
        yield return new WaitForSeconds(1);
        readyText.text = ("GO!");
        yield return new WaitForSeconds(0.5f);
        scrambled = true;
        readyText.text = "";
        TimerDisplay timer = GameObject.Find("Canvas/Text").GetComponent<TimerDisplay>();
        timer.StartClock();
        timer.coroutineRunning = false;
        scrambleBegun = false;
    }
}
