using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour : MonoBehaviour
{
    public Material expectedMaterial; 
    private int correctPieceCount = 0; 
    private const int totalPieces = 9; 
    private bool isComplete = false;
    private MeshRenderer centerRenderer;
    public Transform centerPiece;

    private void Start()
    {
        centerRenderer = centerPiece.GetComponent<MeshRenderer>();
        expectedMaterial = centerRenderer.material; 
        Debug.Log($"Expected Material for {gameObject.name}: {expectedMaterial.name}");    }

    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer pieceRenderer = other.GetComponent<MeshRenderer>();
        Debug.Log(pieceRenderer);
        if (pieceRenderer != null && pieceRenderer.material == expectedMaterial)
        {
            Debug.Log($"This is the correct for {expectedMaterial.name}, {correctPieceCount}");
            correctPieceCount++;
            CheckIfComplete(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MeshRenderer pieceRenderer = other.GetComponent<MeshRenderer>();
        if (pieceRenderer != null && pieceRenderer.material == expectedMaterial)
        {
            correctPieceCount--;
            isComplete = false; 
        }
    }

    private void CheckIfComplete()
    {
        if (correctPieceCount == totalPieces)
        {
            isComplete = true;
            Debug.Log($"{expectedMaterial.name} face is complete!");

            Cube cube = GetComponentInParent<Cube>();
            if (cube != null)
            {
                cube.CheckCubeCompletion();
            }
        }
    }

    public bool IsFaceComplete()
    {
        return isComplete;
    }
}
