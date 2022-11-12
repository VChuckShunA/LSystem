using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AiDirector : MonoBehaviour
{ 
    public PlacementManager placementManager;
    public GameObject[] pedestrianPrefabs;
    private GameObject[] houses;
    private GameObject[] specials;

    public void SpawnAllAagents(){
        foreach (var house in placementManager.GetAllHouses()){
            TrySpawningAnAgent(house, placementManager.GetRandomSpecialStructre());
        }
        foreach (var specialStructure in placementManager.GetAllSpecialStructures()){
            TrySpawningAnAgent(specialStructure, placementManager.GetRandomHouseStructure());
        }
    }
    
   private void TrySpawningAnAgent(GameObject startStructure, GameObject endStructure){
        if (startStructure != null && endStructure != null){
            houses=GameObject.FindGameObjectsWithTag("Houses");
            specials = GameObject.FindGameObjectsWithTag("Special");
            Vector3 startPosition = houses[UnityEngine.Random.Range(0, houses.Length)].transform.position;
            Vector3 endPosition = specials[UnityEngine.Random.Range(0, specials.Length)].transform.position;
            var agent = Instantiate(GetRandomPedestrian(), startPosition, Quaternion.identity);
            var path = placementManager.GetPathBetween(startPosition, endPosition);
            if (path.Count > 0)
            {
                path.Reverse();
                var aiAgent = agent.GetComponent<AIAgent>();
                aiAgent.Initialize(new List<Vector3>(path.Select(x => (Vector3)x).ToList()));
            }
        }
    }

    private GameObject GetRandomPedestrian(){
        return pedestrianPrefabs[UnityEngine.Random.Range(0, pedestrianPrefabs.Length)];
    }
}
