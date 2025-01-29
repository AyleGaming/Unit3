using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Puzzle : MonoBehaviour
{
    protected IPuzzlePiece[] allPuzzlePieces;

    private void Awake()
    {
        // Find all components athat are children of this gameobject
        allPuzzlePieces = GetComponentsInChildren<IPuzzlePiece>();
    }

    public UnityEvent OnPuzzleCompleted;

    public bool isPuzzleComplete;

    public abstract bool CheckSolution();
}
