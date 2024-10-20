using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public Transform cubeParent; // The main parent object holding all cubes
    public Transform rotationParentFront; // Temporary parent for rotating cubes
    public Transform rotationParentBack;
    public Transform rotationParentLeft;
    public Transform rotationParentRight;
    public Transform rotationParentUp;
    public Transform rotationParentDown;
    public Collider frontCollider, backCollider, leftCollider, rightCollider, topCollider, bottomCollider;

    private Dictionary<string, Collider> faceColliders = new Dictionary<string, Collider>();
    
    void Start()
    {
        // Store colliders for each face in a dictionary
        faceColliders.Add("F", frontCollider);
        faceColliders.Add("B", backCollider);
        faceColliders.Add("L", leftCollider);
        faceColliders.Add("R", rightCollider);
        faceColliders.Add("U", topCollider);
        faceColliders.Add("D", bottomCollider);
    }

    void Update()
    {
        // Detect key presses and rotate the corresponding face
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Key pressed");
            RotateFace("F", Vector3.forward, rotationParentFront); // Front face
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            RotateFace("B", Vector3.back, rotationParentBack); // Back face
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            RotateFace("L", Vector3.left, rotationParentLeft); // Left face
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateFace("R", Vector3.right, rotationParentRight); // Right face
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            RotateFace("U", Vector3.up, rotationParentUp); // Up face (top)
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotateFace("D", Vector3.down, rotationParentDown); // Down face (bottom)
        }
    }

    void RotateFace(string faceKey, Vector3 rotationAxis, Transform rotParent)
    {
        // Get the collider for the selected face
        Collider faceCollider = faceColliders[faceKey];

        Debug.Log($"Collider {faceKey} bounds: {faceCollider.bounds}");

        // Find the cubes that are within the face collider
        Vector3 colliderCenter = faceCollider.bounds.center;
        Vector3 colliderSize = faceCollider.bounds.size;

        Collider[] hitColliders = Physics.OverlapBox(colliderCenter, colliderSize / 2);
        List<Transform> faceCubes = new List<Transform>();

        foreach (var hitCollider in hitColliders)
        {

            if (hitCollider.transform.parent == cubeParent)
            {
                faceCubes.Add(hitCollider.transform);
            }
        }

        if (faceCubes.Count == 0)
        {
            Debug.LogWarning($"No cubes detected in the {faceKey} face. Check collider bounds.");
            return;
        }

        // Temporarily parent the cubes to the rotationParent only after the key press
        foreach (Transform cube in faceCubes)
        {
            cube.SetParent(rotParent);
        }

        // Rotate the face (in a coroutine for smooth rotation)
        StartCoroutine(RotateFaceCoroutine(rotationAxis, faceCubes, rotParent));
    }

    System.Collections.IEnumerator RotateFaceCoroutine(Vector3 rotationAxis, List<Transform> faceCubes, Transform rotParent)
    {
        float rotationAmount = 0;
        float rotationSpeed = 100f;
        Quaternion initialRotation = rotParent.rotation;
        Quaternion targetRotation = Quaternion.Euler(rotationAxis * 90) * initialRotation;

        while (rotationAmount < 90)
        {
            float step = rotationSpeed * Time.deltaTime;
            rotParent.Rotate(rotationAxis, step);
            rotationAmount += step;
            yield return null;
        }

        // Snap rotation to exactly 90 degrees
        rotParent.rotation = targetRotation;

        // Unparent the cubes and re-add them to the main parent
        foreach (Transform cube in faceCubes)
        {
            cube.SetParent(cubeParent);
        }

        // Reset rotation parent position and rotation
        rotParent.localRotation = Quaternion.identity;
    }
}
