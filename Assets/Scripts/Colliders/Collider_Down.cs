using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Collider_Down : MonoBehaviour
{
    public Transform downFace;
    public CubeManager cubeManager;
    private List<Transform> cubesToRotate = new List<Transform>();

    private void Start()
    {
        cubeManager = FindObjectOfType<CubeManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Only remove cubes if they are really outside and no longer part of this face.
            // This could use position checks if necessary, for now we just use this placeholder.
            if (cubeManager != null && !cubeManager.IsCubePartOfCurrentRotation(other.transform))
            {
                cubeManager.RemoveCubeToRotate(other.transform);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddCubeToRotate(other.transform);
        }
    }

    private void AddCubeToRotate(Transform cube)
    {
        if (!cubesToRotate.Contains(cube))
        {
            cubesToRotate.Add(cube);  // Add to the list but don't parent it yet
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (cubesToRotate.Count > 0)
            {
                foreach (Transform cube in cubesToRotate)
                {
                    cube.SetParent(downFace);
                }

                cubeManager.RotateDown(cubesToRotate, downFace);
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
        {
            if (cubesToRotate.Count > 0)
            {
                foreach (Transform cube in cubesToRotate)
                {
                    cube.SetParent(downFace);
                }

                cubeManager.RotateDownCounterClockwise(cubesToRotate, downFace);
            }
        }
    }
}
