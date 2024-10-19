using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    public Transform Front_Parent;
    public Transform Cube_Parent;
    public Vector3 mPrevPos = Vector3.zero;
    public Vector3 mPosChange = Vector3.zero;
    //public int sidesCompleted;

    public Colour whichColour;

    public bool greenComplete;
    public bool blueComplete;
    public bool redComplete;
    public bool orangeComplete;
    public bool whiteComplete;
    public bool yellowComplete;
    public TimerDisplay timer;

    private void Start()
    {
        timer = GameObject.Find("Canvas/Text").GetComponent<TimerDisplay>();
    }

    void Update()
    {
        //identifys whether each side is complete
        redComplete = GameObject.Find("Rubik's_Cube/Centre_14_r/Red_Cube/Extra_Collider").
            GetComponent<Colour>().redComplete;
        greenComplete = GameObject.Find("Rubik's_Cube/Centre_5_g/Green_Cube/Extra_Collider").
            GetComponent<Colour>().greenComplete;
        blueComplete = GameObject.Find("Rubik's_Cube/Centre_22_b/Cube/Extra_Collider").
            GetComponent<Colour>().blueComplete;
        orangeComplete = GameObject.Find("Rubik's_Cube/Centre_13_o/Cube/Extra_Collider").
            GetComponent<Colour>().orangeComplete;
        whiteComplete = GameObject.Find("Rubik's_Cube/Centre_11_w/Cube/Extra_Collider").
            GetComponent<Colour>().whiteComplete;
        yellowComplete = GameObject.Find("Rubik's_Cube/Centre_16_y/Cube/Extra_Collider").
            GetComponent<Colour>().yellowComplete;
        //have to call each scripts component seperately otherwise only one is actuallly recognised.
        

        if (redComplete && greenComplete && blueComplete &&
            orangeComplete && whiteComplete && yellowComplete)
        {
            timer.StopClock();
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(FullRotateVerticalU());
            
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(FullRotateVerticalD());

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(FullRotateHorizontalL());

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(FullRotateHorizontalR());

        }
    }

    public IEnumerator FullRotateVerticalU()
    {
        for (int x = 0; x < 9; x++)
        {
            //the SPACE.WORLD function fixed my rotation issues, so that the rotations
            //are no longer taking place across the cubes constantly moving axis but instead
            //sin the worlds stationary axis
            transform.Rotate(10, 0, 0, Space.World);
            yield return null;
        }
    }
    public IEnumerator FullRotateVerticalD()
    {
        for (int x = 0; x < 9; x++)
        {
            this.transform.Rotate(-10, 0, 0, Space.World);
            yield return null;
        }
    }

    public IEnumerator FullRotateHorizontalR()
    {
        for (int x = 0; x < 9; x++)
        {
            this.transform.Rotate(0, -10, 0, Space.World);
            yield return null;
        }
    }
    public IEnumerator FullRotateHorizontalL()
    {
        for (int x = 0; x < 9; x++)
        {
            transform.Rotate(0, 10, 0, Space.World);
            yield return null;
        }
    }
}
