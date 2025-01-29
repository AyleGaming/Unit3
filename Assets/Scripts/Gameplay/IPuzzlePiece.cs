using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// An interface gives ability of any monobehaviour to be linked to a puzzle
// consecutively, to be checked if its correct
public interface IPuzzlePiece
{
    public void LinkToPuzzle(Puzzle p);
    public bool IsCorrect();
}
