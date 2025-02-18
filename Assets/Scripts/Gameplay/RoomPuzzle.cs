using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPuzzle : Puzzle
{
    private void Update()
    {
        if (CheckSolution() && isPuzzleComplete == false)
        {
            OnPuzzleCompleted?.Invoke();
            isPuzzleComplete = true;
        }
    }

    public override bool CheckSolution()
    {
        foreach(IPuzzlePiece piece in allPuzzlePieces)
        {
            if (!piece.IsCorrect())
            {
                if(isPuzzleComplete == true)
                {
                    isPuzzleComplete = false;
                    OnPuzzleIncompleted?.Invoke();
                }
                return false;
            }
        }
        return true;
    }

}
