using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
//    [SerializeField] private bool isIndicatorActive = false;
    [SerializeField] private GameObject colorIndicator; 

    public void SetIndicatorActive()
    {
        //      isIndicatorActive = true;
        colorIndicator.GetComponent<Renderer>().material.color = Color.green;
    }

    public void SetIndicatorInactive()
    {
        //    isIndicatorActive = false;
        colorIndicator.GetComponent<Renderer>().material.color = Color.red;
    }
}
