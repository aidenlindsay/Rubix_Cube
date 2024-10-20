using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Front : MonoBehaviour
{
    public Transform frontFace;
    public CubeManager cubeManager;
    private List<Transform> cubesToRotate = new List<Transform>();

    private void Start()
    {
        cubeManager = FindObjectOfType<CubeManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddCubeToRotate(other.transform);
        }
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


    private void AddCubeToRotate(Transform cube)
    {
        if (!cubesToRotate.Contains(cube))
        {
            cubesToRotate.Add(cube);  // Add to the list but don't parent it yet
        }
    }

    private void Update()
    {
        // Check for player input to rotate the front face
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (cubesToRotate.Count > 0) // Only proceed if exactly 9 cubes are ready for rotation
            {
                foreach (Transform cube in cubesToRotate)
                {
                    cube.SetParent(frontFace);  // Only parent them when the rotation is triggered
                }

                Debug.Log($"Rotating {cubesToRotate.Count} cubes");
                cubeManager.RotateFront(cubesToRotate, frontFace); // Pass the list and the parent (face)
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F))
        {
            //wasn't working before when equals to 9
            if (cubesToRotate.Count > 0)
            {
                foreach (Transform cube in cubesToRotate)
                {
                    cube.SetParent(frontFace);
                }

                cubeManager.RotateFrontCounterClockwise(cubesToRotate, frontFace);
            }
        }
    }
}