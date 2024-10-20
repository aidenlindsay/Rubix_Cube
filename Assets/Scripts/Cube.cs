using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{

    private bool isRotating = false;
    public bool greenComplete;
    public bool blueComplete;
    public bool redComplete;
    public bool orangeComplete;
    public bool whiteComplete;
    public bool yellowComplete;
    public TimerDisplay timer;

    private GameObject redCube;
    private GameObject greenCube;
    private GameObject blueCube;
    private GameObject orangeCube;
    private GameObject whiteCube;
    private GameObject yellowCube;

    private Colour redColour;
    private Colour greenColour;
    private Colour blueColour;
    private Colour orangeColour;
    private Colour whiteColour;
    private Colour yellowColour;

    private void Start()
    {
        timer = GameObject.Find("Canvas/Text").GetComponent<TimerDisplay>();

        redCube = GameObject.Find("Rubik's_Cube/Centre_14_r/Red_Cube/Extra_Collider");
        greenCube = GameObject.Find("Rubik's_Cube/Centre_5_g/Green_Cube/Extra_Collider");
        blueCube = GameObject.Find("Rubik's_Cube/Centre_22_b/Cube/Extra_Collider");
        orangeCube = GameObject.Find("Rubik's_Cube/Centre_13_o/Cube/Extra_Collider");
        whiteCube = GameObject.Find("Rubik's_Cube/Centre_11_w/Cube/Extra_Collider");
        yellowCube = GameObject.Find("Rubik's_Cube/Centre_16_y/Cube/Extra_Collider");

        if (redCube != null) redColour = redCube.GetComponent<Colour>();
        if (greenCube != null) greenColour = greenCube.GetComponent<Colour>();
        if (blueCube != null) blueColour = blueCube.GetComponent<Colour>();
        if (orangeCube != null) orangeColour = orangeCube.GetComponent<Colour>();
        if (whiteCube != null) whiteColour = whiteCube.GetComponent<Colour>();
        if (yellowCube != null) yellowColour = yellowCube.GetComponent<Colour>();
    }

    void Update()
    {
        if (redColour != null) redComplete = redColour.redComplete;
        if (greenColour != null) greenComplete = greenColour.greenComplete;
        if (blueColour != null) blueComplete = blueColour.blueComplete;
        if (orangeColour != null) orangeComplete = orangeColour.orangeComplete;
        if (whiteColour != null) whiteComplete = whiteColour.whiteComplete;
        if (yellowColour != null) yellowComplete = yellowColour.yellowComplete;

        if (redComplete && greenComplete && blueComplete &&
            orangeComplete && whiteComplete && yellowComplete)
        {
            timer.StopClock();
        }

        if (!isRotating){
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
    }

    public IEnumerator FullRotateVerticalU()
    {
        if (isRotating) yield break;
        isRotating = true;

        for (int x = 0; x < 18; x++)
        {
            //the SPACE.WORLD function fixed my rotation issues, so that the rotations
            //are no longer taking place across the cubes constantly moving axis but instead
            //sin the worlds stationary axis
            transform.Rotate(5, 0, 0, Space.World);
            yield return new WaitForSeconds(0.01f);
        }
        isRotating = false;
    }
    public IEnumerator FullRotateVerticalD()
    {
        if (isRotating) yield break;
        isRotating = true;

        for (int x = 0; x < 18; x++)
        {
            this.transform.Rotate(-5, 0, 0, Space.World);
            yield return new WaitForSeconds(0.01f);
        }
        isRotating = false;
    }

    public IEnumerator FullRotateHorizontalR()
    {
        if (isRotating) yield break;
        isRotating = true;

        for (int x = 0; x < 18; x++)
        {
            this.transform.Rotate(0, -5, 0, Space.World);
            yield return new WaitForSeconds(0.01f);
        }
        isRotating = false;
    }
    public IEnumerator FullRotateHorizontalL()
    {
        if (isRotating) yield break;
        isRotating = true;

        for (int x = 0; x < 18; x++)
        {
            transform.Rotate(0, 5, 0, Space.World);
            yield return new WaitForSeconds(0.01f);
        }
        isRotating = false;
    }
}
