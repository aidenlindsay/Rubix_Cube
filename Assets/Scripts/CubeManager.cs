using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public Transform frontFace;
    public Transform backFace;
    public Transform leftFace;
    public Transform rightFace;
    public Transform upFace;
    public Transform downFace;

    public Transform mainCube;

    public float rotationSpeed = 1f;

    public List<Transform> cubesToRotate = new List<Transform>();

    public void RemoveCubeToRotate(Transform cube)
    {
        if (cubesToRotate.Contains(cube))
        {
            cubesToRotate.Remove(cube);
            cube.SetParent(mainCube); // Unparent the cube when it exits the collider
        }
    }

    public void RotateFront(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(frontFace, Vector3.back, cubes));
    }

    public void RotateBack(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(backFace, Vector3.forward, cubes));
    }

    public void RotateLeft(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(leftFace, Vector3.right, cubes));
    }

    public void RotateRight(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(rightFace, Vector3.left, cubes));
    }

    public void RotateUp(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(upFace, Vector3.up, cubes));
    }

    public void RotateDown(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(downFace, Vector3.down, cubes));
    }

    // Methods to rotate the faces counter-clockwise
    public void RotateFrontCounterClockwise(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(frontFace, -Vector3.back, cubes));
    }

    public void RotateBackCounterClockwise(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(backFace, -Vector3.forward, cubes));
    }

    public void RotateLeftCounterClockwise(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(leftFace, -Vector3.right, cubes));
    }

    public void RotateRightCounterClockwise(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(rightFace, -Vector3.left, cubes));
    }

    public void RotateUpCounterClockwise(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(upFace, -Vector3.up, cubes));
    }

    public void RotateDownCounterClockwise(List<Transform> cubes)
    {
        StartCoroutine(RotateFace(downFace, -Vector3.down, cubes));
    }

    private IEnumerator RotateFace(Transform face, Vector3 direction, List<Transform> cubesToRotate)
    {
        float totalRotation = 0f; // Keep track of the total rotation
        float targetRotation = 90f; // Target rotation
        float step = rotationSpeed * Time.deltaTime; // Rotation step

        // Continue rotating until the total rotation reaches or exceeds the target rotation
        while (totalRotation < targetRotation)
        {
            // Calculate the rotation for this frame
            float rotationThisFrame = Mathf.Min(step, targetRotation - totalRotation);
            face.Rotate(direction * rotationThisFrame);
            totalRotation += rotationThisFrame; // Update the total rotation
            yield return null; // Wait for the next frame
        }

        // After rotation, unparent the cubes from the front face
        foreach (Transform cube in cubesToRotate)
        {
            cube.SetParent(mainCube); // Move back to the original hierarchy
        }

        // Clear the list for the next rotation
        cubesToRotate = new List<Transform>();
    }
}