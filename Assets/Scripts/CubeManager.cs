using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public Transform mainCube;
    public float rotationSpeed = 90f;

    private Coroutine currentRotationCoroutine;

    private List<Transform> currentlyRotatingCubes = new List<Transform>();
    private List<Transform> cubesToRotate = new List<Transform>();

    public void RemoveCubeToRotate(Transform cube)
    {
        // Check if the cube is still part of the rotation list
        if (cubesToRotate.Contains(cube))
        {
            cube.SetParent(mainCube); // Reassign it back to the main cube
            cubesToRotate.Remove(cube); // Remove from the list
        }
    }

    public bool IsCubePartOfCurrentRotation(Transform cube)
    {
        // Check if the cube is part of the currently rotating cubes
        return currentlyRotatingCubes.Contains(cube);
    }

    // Remove any ongoing rotation coroutine
    private void StopCurrentRotation()
    {
        if (currentRotationCoroutine != null)
        {
            StopCoroutine(currentRotationCoroutine);
            currentRotationCoroutine = null;
        }
    }

    // Rotate the front face clockwise
    public void RotateFront(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation(); // Ensure no other rotation is running
        currentRotationCoroutine = StartCoroutine(RotateFace(face, Vector3.back, cubes));
    }

    public void RotateBack(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation();
        currentRotationCoroutine = StartCoroutine(RotateFace(face, Vector3.forward, cubes));
    }

    public void RotateLeft(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation();
        currentRotationCoroutine = StartCoroutine(RotateFace(face, Vector3.right, cubes));
    }

    public void RotateRight(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation();
        currentRotationCoroutine = StartCoroutine(RotateFace(face, Vector3.left, cubes));
    }

    public void RotateUp(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation();
        currentRotationCoroutine = StartCoroutine(RotateFace(face, Vector3.up, cubes));
    }

    public void RotateDown(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation();
        currentRotationCoroutine = StartCoroutine(RotateFace(face, Vector3.down, cubes));
    }

    // Rotate the front face counter-clockwise
    public void RotateFrontCounterClockwise(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation(); // Ensure no other rotation is running
        currentRotationCoroutine = StartCoroutine(RotateFace(face, -Vector3.back, cubes));
    }

    public void RotateBackCounterClockwise(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation();
        currentRotationCoroutine = StartCoroutine(RotateFace(face, -Vector3.forward, cubes));
    }

    public void RotateLeftCounterClockwise(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation();
        currentRotationCoroutine = StartCoroutine(RotateFace(face, -Vector3.right, cubes));
    }

    public void RotateRightCounterClockwise(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation();
        currentRotationCoroutine = StartCoroutine(RotateFace(face, -Vector3.left, cubes));
    }

    public void RotateUpCounterClockwise(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation();
        currentRotationCoroutine = StartCoroutine(RotateFace(face, -Vector3.up, cubes));
    }

    public void RotateDownCounterClockwise(List<Transform> cubes, Transform face)
    {
        StopCurrentRotation();
        currentRotationCoroutine = StartCoroutine(RotateFace(face, -Vector3.down, cubes));
    }

    // Generic method to rotate a face
    private IEnumerator RotateFace(Transform face, Vector3 rotationAxis, List<Transform> cubes)
    {
        float totalRotation = 0f;
        float targetRotation = 90f;

        // Rotate the face as a whole
        while (totalRotation < targetRotation)
        {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            rotationThisFrame = Mathf.Min(rotationThisFrame, targetRotation - totalRotation);

            face.Rotate(rotationAxis * rotationThisFrame, Space.World); // Rotate the entire face
            totalRotation += rotationThisFrame;

            yield return null; // Wait for the next frame
        }

        // After rotation is complete, return cubes to mainCube
        foreach (Transform cube in cubes)
        {
            cube.SetParent(mainCube);
        }

        cubes.Clear(); // Clear the list
        currentRotationCoroutine = null; // Reset coroutine reference
    }
}
