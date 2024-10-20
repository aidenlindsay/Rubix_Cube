using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colour : MonoBehaviour
{
    public string thisColour;
    public GameObject centreColour;
    public int colourCount = 0;
    public string sideComplete = "null";
    public bool greenComplete = false;
    public bool blueComplete = false;
    public bool yellowComplete = false;
    public bool whiteComplete = false;
    public bool redComplete = false;
    public bool orangeComplete = false;

    void Start()
    {
        thisColour = GetComponentInParent<MeshRenderer>().material.ToString();
        //colourCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.U)
            || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Clutch());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        RandomScript scramble = GameObject.Find("Rubik's_Cube").GetComponent<RandomScript>();

        if (scramble.scrambled == true)
        {
            if (other.GetComponent<MeshRenderer>().material.ToString() == thisColour)
            {
                colourCount++;
                //print("yup");
            }
        }   
        //print(colourCount);

        if (colourCount == 8)
        {
            changeStatePos();
        }
        else
        {
            changeStateNeg();
        }
    }

    public IEnumerator Clutch()
    {
        this.gameObject.transform.Translate(1,0,0);
        colourCount = 0;
        yield return new WaitForSeconds(0.2f);
        this.gameObject.transform.Translate(-1,0,0);
    }

    public void changeStatePos()
    {
        if (transform.parent.tag == "Green")
        {
            //print(thisColour + "has finished");
            greenComplete = true;
        }
        else if (transform.parent.tag == "Blue")
        {
            //print(thisColour + "has finished");
            blueComplete = true;
        }
        else if (transform.parent.tag == "Red")
        {
            //print(thisColour + "has finished");
            redComplete = true;
        }
        else if (transform.parent.tag == "Orange")
        {
            //print(thisColour + "has finished");
            orangeComplete = true;
        }
        else if (transform.parent.tag == "Yellow")
        {
            //print(thisColour + "has finished");
            yellowComplete = true;
        }
        else if (transform.parent.tag == "White")
        {
            //print(thisColour + "has finished");
            whiteComplete = true;
        }
    }

    public void changeStateNeg()
    {
        if (transform.parent.tag == "Green")
        {
            //print(thisColour + "has finished");
            greenComplete = false;
        }
        else if (transform.parent.tag == "Blue")
        {
            //print(thisColour + "has finished");
            blueComplete = false;
        }
        else if (transform.parent.tag == "Red")
        {
            //print(thisColour + "has finished");
            redComplete = false;
        }
        else if (transform.parent.tag == "Orange")
        {
            //print(thisColour + "has finished");
            orangeComplete = false;
        }
        else if (transform.parent.tag == "Yellow")
        {
            //print(thisColour + "has finished");
            yellowComplete = false;
        }
        else if (transform.parent.tag == "White")
        {
            //print(thisColour + "has finished");
            whiteComplete = false;
        }
    }
}
