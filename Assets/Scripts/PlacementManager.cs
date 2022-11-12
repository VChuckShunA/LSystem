using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    Grid placementGrid;
    internal List<Vector3> GetPathBetween(Vector3 startPosition, Vector3 endPosition)
    {
        var resultPath = GridSearch.AStarSearch(placementGrid, new Point((int)startPosition.x, (int)startPosition.z), new Point((int)endPosition.x, (int)endPosition.z));
        List<Vector3> path = new List<Vector3>();
        foreach (Point point in resultPath)
        {
            path.Add(new Vector3(point.X, 0, point.Y));
        }
        return path;
    }

    public GameObject[] GetAllHouses() {

        GameObject[] houses = GameObject.FindGameObjectsWithTag("Houses");
        return houses;
    }
    public GameObject[] GetAllSpecialStructures(){
        GameObject[] specials = GameObject.FindGameObjectsWithTag("Special");
        return specials;
    }

    public GameObject GetRandomSpecialStructre() {
        GameObject[] specials = GameObject.FindGameObjectsWithTag("Special");
        GameObject special = specials[UnityEngine.Random.Range(0, specials.Length)];
        return special;
    }

    public GameObject GetRandomHouseStructure(){
        GameObject[] houses = GameObject.FindGameObjectsWithTag("Houses");
        GameObject house = houses[UnityEngine.Random.Range(0, houses.Length)];
        return house;
    }

}
