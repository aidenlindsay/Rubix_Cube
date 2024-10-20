﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Back : MonoBehaviour
{
    public Transform backFace;
    public CubeManager cubeManager;
    private List<Transform> cubesToRotate = new List<Transform>();

    private void Start()
    {
        // Get the CubeManager component from the parent object
        cubeManager = FindObjectOfType<CubeManager>();
    }

    public void AddCubeToRotate(Transform cube)
    {
        if (!cubesToRotate.Contains(cube))
        {
            cubesToRotate.Add(cube);
            //cube.SetParent(backFace); // Parent the cube to the front face
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a piece of the Rubik's Cube
        if (other.CompareTag("Player"))
        {
            // Add this piece to the CubeManager's list of cubes to rotate
            AddCubeToRotate(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to a piece of the Rubik's Cube
        if (other.CompareTag("Player"))
        {
            // Remove this piece from the CubeManager's list of cubes to rotate
            cubeManager.RemoveCubeToRotate(other.transform);
        }
    }

    private void Update()
    {
        // Check for player input to rotate the up face
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (cubesToRotate.Count > 0)
            {
                foreach (Transform cube in cubesToRotate)
                {
                    cube.SetParent(backFace);
                }
                cubeManager.RotateBack(cubesToRotate);
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.B))
        {
            if (cubesToRotate.Count > 0)
            {
                foreach (Transform cube in cubesToRotate)
                {
                    cube.SetParent(backFace);
                }
                cubeManager.RotateBackCounterClockwise(cubesToRotate);
            }
        }
    }
}
