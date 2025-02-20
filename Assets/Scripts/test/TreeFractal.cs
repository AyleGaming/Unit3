using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFractal : MonoBehaviour
{
    public GameObject branchPrefab;
    public int amountOfBranches;
    public int angleToRotateBranch;
    public float distanceOffset = 1;
    private float sizeDecreaseMultiplier = 0.6f;
    private float minSizeOfBranch = 0.1f;

    void Start()
    {
        SpawnBranch(gameObject, 0, 0);
    }


    public void SpawnBranch(GameObject currentBranch, int branchCounter, float angleToRotate)
    {

        if(currentBranch.transform.localScale.x <= minSizeOfBranch)
        {
            return;
        }

        if (branchCounter >= amountOfBranches)
        {
            return;
        }

        Vector3 branchPosition = currentBranch.transform.position;

        if(branchCounter != 0)
        {
            branchPosition += currentBranch.transform.up * distanceOffset * currentBranch.transform.localScale.x;
        }

        GameObject newBranch = Instantiate(branchPrefab, branchPosition, currentBranch.transform.rotation);

        // Apply rotation
        newBranch.transform.Rotate(angleToRotate, 0, 0);
        newBranch.transform.localScale = currentBranch.transform.localScale * sizeDecreaseMultiplier;

        SpawnBranch(newBranch, branchCounter + 1, +angleToRotateBranch);
        SpawnBranch(newBranch, branchCounter + 1, -angleToRotateBranch);
    }
}
