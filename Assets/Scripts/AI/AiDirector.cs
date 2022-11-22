using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleCity.AI
{
    public class AiDirector : MonoBehaviour
    {
        public PlacementManager placementManager;
        public GameObject[] pedestrianPrefabs;

        public void SpawnAllAagents()
        {
           /* Debug.Log("All Houeses: "+placementManager.GetAllHouses());
            Debug.Log("Random Special Structure: "+placementManager.GetRandomSpecialStrucutre());
            Debug.Log("All Special Structures: "+placementManager.GetAllSpecialStructures());
            Debug.Log("Random House Structure: "+placementManager.GetRandomHouseStructure());*/
            foreach (var house in placementManager.GetAllHouses())
            {
                Debug.Log("For loop"+house);
                TrySpawningAnAgent(house, placementManager.GetRandomSpecialStrucutre());
            }
            foreach (var specialStructure in placementManager.GetAllSpecialStructures())
            {
                TrySpawningAnAgent(specialStructure, placementManager.GetRandomHouseStructure());
            }
        }

        private void TrySpawningAnAgent(Point startStructure, Point endStructure)
        {
            if(startStructure != null && endStructure != null)
            {
                var startPosition =startStructure;//.RoadPosition; //= ((INeedingRoad)startStructure).RoadPosition;
                var endPosition = endStructure;//.RoadPosition; //((INeedingRoad)endStructure).RoadPosition;
                var agent = Instantiate(GetRandomPedestrian(), new Vector3(startPosition.X,0,startPosition.Y), Quaternion.identity);
                var path = placementManager.GetPathBetween(new Vector3Int(startPosition.X,0,startPosition.Y), new Vector3Int(endPosition.X,0,endPosition.Y));
                if(path.Count > 0)
                {
                    path.Reverse();
                    var aiAgent = agent.GetComponent<AiAgent>();
                    aiAgent.Initialize(new List<Vector3>(path.Select(x => (Vector3)x).ToList()));
                }
            }
        }

        private GameObject GetRandomPedestrian()
        {
            return pedestrianPrefabs[UnityEngine.Random.Range(0, pedestrianPrefabs.Length)];
        }
    }
}

