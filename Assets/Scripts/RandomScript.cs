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

    public CubeManager cubeManager;

    private void Start()
    {
        cubeManager = GameObject.Find("CubeManager").GetComponent<CubeManager>();
    }

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
                    cubeManager.RotateFace("F", -Vector3.forward, cubeManager.rotationParentFront);
                    break;
                case 2:
                    cubeManager.RotateFace("B", -Vector3.back, cubeManager.rotationParentBack);
                    break;
                case 3:
                    cubeManager.RotateFace("L", Vector3.left, cubeManager.rotationParentLeft);
                    break;
                case 4:
                    cubeManager.RotateFace("R", Vector3.right, cubeManager.rotationParentRight);
                    break;
                case 5:
                    cubeManager.RotateFace("U", Vector3.up, cubeManager.rotationParentUp);
                    break;
                case 6:
                    cubeManager.RotateFace("D", Vector3.down, cubeManager.rotationParentDown);
                    break;
            }
            yield return new WaitUntil(() => !cubeManager.isRotating);
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
