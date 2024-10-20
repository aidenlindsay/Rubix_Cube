using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public Transform cubeParent;
    public Transform rotationParentFront; 
    public Transform rotationParentBack;
    public Transform rotationParentLeft;
    public Transform rotationParentRight;
    public Transform rotationParentUp;
    public Transform rotationParentDown;
    public Collider frontCollider, backCollider, leftCollider, rightCollider, topCollider, bottomCollider;

    private Dictionary<string, Collider> faceColliders = new Dictionary<string, Collider>();

    public float rotationSpeed = 300f;
    public Slider speedSlider;


    public Colour[] colourColliders;
    private bool isCubeComplete = false;

    public bool isRotating = false;
    
    void Start()
    {

        colourColliders = GetComponentsInChildren<Colour>();

        if (speedSlider != null)
        {
            speedSlider.value = rotationSpeed;
            speedSlider.onValueChanged.AddListener(UpdateRotationSpeed);
        }

        faceColliders.Add("F", frontCollider);
        faceColliders.Add("B", backCollider);
        faceColliders.Add("L", leftCollider);
        faceColliders.Add("R", rightCollider);
        faceColliders.Add("U", topCollider);
        faceColliders.Add("D", bottomCollider);
    }

    void Update()
    {
        if (isRotating) return;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Key pressed");
            RotateFace("F", Vector3.forward, rotationParentFront); 
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.B))
        {
            RotateFace("B", Vector3.back, rotationParentBack); 
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.L))
        {
            RotateFace("L", -Vector3.left, rotationParentLeft); 
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            RotateFace("R", -Vector3.right, rotationParentRight);
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.U))
        {
            RotateFace("U", -Vector3.up, rotationParentUp);
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
        {
            RotateFace("D", -Vector3.down, rotationParentDown);
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Key pressed");
            RotateFace("F", -Vector3.forward, rotationParentFront);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            RotateFace("B", -Vector3.back, rotationParentBack);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            RotateFace("L", Vector3.left, rotationParentLeft);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateFace("R", Vector3.right, rotationParentRight);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            RotateFace("U", Vector3.up, rotationParentUp); 
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotateFace("D", Vector3.down, rotationParentDown);
        }
    }

    public void RotateFace(string faceKey, Vector3 rotationAxis, Transform rotParent)
    {
        Collider faceCollider = faceColliders[faceKey];

        Debug.Log($"Collider {faceKey} bounds: {faceCollider.bounds}");

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

        foreach (Transform cube in faceCubes)
        {
            cube.SetParent(rotParent);
        }

        isRotating = true;
        StartCoroutine(RotateFaceCoroutine(rotationAxis, faceCubes, rotParent));
    }

    System.Collections.IEnumerator RotateFaceCoroutine(Vector3 rotationAxis, List<Transform> faceCubes, Transform rotParent)
    {
        float rotationAmount = 0;
        Quaternion initialRotation = rotParent.rotation;
        Quaternion targetRotation = Quaternion.Euler(rotationAxis * 90) * initialRotation;
        isRotating = true;

        while (rotationAmount < 90)
        {
            float step = rotationSpeed * Time.deltaTime;
            rotParent.Rotate(rotationAxis, step);
            rotationAmount += step;
            yield return null;
        }

        rotParent.rotation = targetRotation;

        foreach (Transform cube in faceCubes)
        {
            cube.SetParent(cubeParent);
        }

        rotParent.localRotation = Quaternion.identity;
        isRotating = false;
    }

    public void UpdateRotationSpeed(float newSpeed)
    {
        rotationSpeed = newSpeed;
        Debug.Log("Updated Rotation Speed: " + rotationSpeed);
    }

    public void CheckCubeCompletion()
    {
        isCubeComplete = true; 

        foreach (Colour collider in colourColliders)
        {
            if (!collider.IsFaceComplete())
            {
                isCubeComplete = false;
                break; // No need to check further if one face is incomplete
            }
        }

        if (isCubeComplete)
        {
            Debug.Log("The cube is complete!");
            TimerDisplay timer = GameObject.Find("Canvas/Text").GetComponent<TimerDisplay>();
            timer.StopClock(); 
        }
    }
}
