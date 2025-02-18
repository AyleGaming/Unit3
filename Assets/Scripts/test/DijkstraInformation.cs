using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class DijkstraInformation
{
    public GraphNode cityNode;
    public float shortestDistanceTo;
    public GraphNode previousCityTogo;
}
