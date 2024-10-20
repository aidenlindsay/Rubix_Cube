using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool isRotating = false;
    public TimerDisplay timer;
    private Colour[] faceColors;

    private void Start()
    {
        timer = GameObject.Find("Canvas/Text").GetComponent<TimerDisplay>();
        faceColors = GetComponentsInChildren<Colour>();
    }

    void Update()
    {
        if (!isRotating)
        {
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

    public void CheckCubeCompletion()
    {
        bool allFacesComplete = true;

        foreach (Colour face in faceColors)
        {
            if (!face.IsFaceComplete())
            {
                allFacesComplete = false;
                break; // Exit early if any face is not complete
            }
        }

        if (allFacesComplete)
        {
            timer.StopClock();
            Debug.Log("Cube is solved!");
        }
    }

    public IEnumerator FullRotateVerticalU()
    {
        if (isRotating) yield break;
        isRotating = true;

        for (int x = 0; x < 18; x++)
        {
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
            transform.Rotate(-5, 0, 0, Space.World);
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
            transform.Rotate(0, -5, 0, Space.World);
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

