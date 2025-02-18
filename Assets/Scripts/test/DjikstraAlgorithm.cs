using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DjikstraAlgorithm : MonoBehaviour
{
    [SerializeField] private List<DijkstraInformation> cities;
    [SerializeField] private GraphNode startingNode;

    private List<DijkstraInformation> unvisitedCities = new List<DijkstraInformation>();
    private List<DijkstraInformation> visitedCities = new List<DijkstraInformation>();


    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DoDijkstraAlgorithm(GetInformationOnTableFromGraphNode(startingNode, cities));
        }
    }

    private void DoDijkstraAlgorithm(DijkstraInformation startingNode)
    {
        unvisitedCities.AddRange(cities);


        foreach (DijkstraInformation city in unvisitedCities)
        {
            if (city == startingNode)
            {
                city.shortestDistanceTo = 0;
            }
            else
            {
                city.shortestDistanceTo = Mathf.Infinity;
            }
        }

        while(unvisitedCities.Count > 0)
        {
            DijkstraInformation currentCity = GetCityInfoWithShortestDistance(unvisitedCities);

            foreach (GraphNode neighbour in currentCity.cityNode.connectedNodes)
            {
                DijkstraInformation infoAboutNeighbour = GetInformationOnTableFromGraphNode(neighbour, unvisitedCities);

                if (infoAboutNeighbour != null)
                {
                    float distanceToNode = Vector3.Distance(neighbour.transform.position, currentCity.cityNode.transform.position);
                    if (distanceToNode < cities[cities.IndexOf(infoAboutNeighbour)].shortestDistanceTo)
                    {
                        // UpdateTable();
                        infoAboutNeighbour.shortestDistanceTo = distanceToNode + currentCity.shortestDistanceTo;
                        infoAboutNeighbour.previousCityTogo = currentCity.cityNode;
                    }
                }
            }

            unvisitedCities.Remove(currentCity);
            visitedCities.Add(currentCity);
        }
    }


    private DijkstraInformation GetCityInfoWithShortestDistance(List<DijkstraInformation> cityInfos)
    {
        float lowestDistance = Mathf.Infinity;
        DijkstraInformation infoWithLowestDistance = null;

        foreach (DijkstraInformation info in cityInfos)
        {
            if (info.shortestDistanceTo < lowestDistance)
            {
                lowestDistance = info.shortestDistanceTo;
                infoWithLowestDistance = info;
                // info is now the city with lowest distance
            }
        }

        return infoWithLowestDistance;

    }

    private DijkstraInformation GetInformationOnTableFromGraphNode(GraphNode node, List<DijkstraInformation> cityInfos)
    {
        foreach(DijkstraInformation info in cityInfos)
        {
            if(info.cityNode == node)
            {
                return info;
            }
        }
        return null;
    }

}
