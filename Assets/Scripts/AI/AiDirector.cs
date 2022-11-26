using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
                TrySpawningAnAgent(house, placementManager.GetRandomSpecialStrucutre());
                //Debug.Log("house "+house);
            }
            foreach (var specialStructure in placementManager.GetAllSpecialStructures())
            {
                TrySpawningAnAgent(specialStructure, placementManager.GetRandomHouseStructure());
                //Debug.Log("special "+specialStructure);
            }
        }

        private void TrySpawningAnAgent(Point startStructure, Point endStructure)
        {
            if(startStructure != null && endStructure != null)
            {
                var startPosition = placementManager.GetNearestRoad(new Vector3Int(startStructure.X,0,startStructure.Y),1,1).Value; //= ((INeedingRoad)startStructure).RoadPosition;
                var endPosition = placementManager.GetNearestRoad(new Vector3Int(endStructure.X,0,endStructure.Y),1,1).Value; //((INeedingRoad)endStructure).RoadPosition;
                
                var agent = Instantiate(GetRandomPedestrian(), new Vector3Int(startPosition.x,0,startPosition.z), Quaternion.identity);
                //Debug.Log(startPosition);
                var path = placementManager.GetPathBetween(new Vector3Int(startPosition.x,0,startPosition.z), new Vector3Int(endPosition.x,0,endPosition.z));
               
                
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
