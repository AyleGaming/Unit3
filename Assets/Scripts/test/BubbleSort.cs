using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : MonoBehaviour
{
    public int[] sortedArray;

    void Start()
    {
        int N = sortedArray.Length;
        int sortedElements = 0;

        while (sortedElements < N)
        {
            int currentElement = 0;

            while(currentElement <= N - 2 - sortedElements)
            {
                if(sortedArray[currentElement] > sortedArray[currentElement + 1])
                {
                    int tempVariable = sortedArray[currentElement];
                    sortedArray[currentElement] = sortedArray[currentElement + 1];
                    sortedArray[currentElement + 1] = tempVariable;

                }
                currentElement += 1;
            }
            sortedElements += 1;
        }
    }
}
