using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureHelper : MonoBehaviour
{

    public HouseType[] buildingTypes;
    public Dictionary<Vector3Int, GameObject> structuresDictionary = new Dictionary<Vector3Int, GameObject>();


    public void PlaceStructuresAroundRoad(List<Vector3Int> roadPositions) {
        Dictionary<Vector3Int, Direction> freeEstateSpots = FindFreeSpaceAroundRoad(roadPositions);
        List<Vector3Int> blockedPositions = new List<Vector3Int>();
        foreach (var freeSpot in freeEstateSpots) {
            if (blockedPositions.Contains(freeSpot.Key)) {
                continue;
            }
            var rotation = Quaternion.identity;
            switch (freeSpot.Value) {
                case Direction.Up:
                    rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case Direction.Down:
                    rotation = Quaternion.Euler(0, -90, 0);
                    break;
                case Direction.Right:
                    rotation = Quaternion.Euler(0, 180, 0);
                    break;
                default:
                    break;
            }
            //We need to loop through each building types
            for (int i = 0; i < buildingTypes.Length; i++) {
                if (buildingTypes[i].quantity == -1) {
                    var building = SpawnPrefab(buildingTypes[i].GetPrefab(), freeSpot.Key, rotation);
                    structuresDictionary.Add(freeSpot.Key, building);
                    break;
                }
                if (buildingTypes[i].IsBuildingAvailable()) {
                    if (buildingTypes[i].sizeRequired > 1) {
                        var halfSize = Mathf.CeilToInt(buildingTypes[i].sizeRequired/2.0f);
                        List<Vector3Int> tempPositionsBlocked = new List<Vector3Int>();
                        if (VerifyIfBuildingFits(halfSize,freeEstateSpots,freeSpot, ref tempPositionsBlocked)) { 
                            blockedPositions.AddRange(tempPositionsBlocked);
                            var building = SpawnPrefab(buildingTypes[i].GetPrefab(), freeSpot.Key, rotation);
                            structuresDictionary.Add(freeSpot.Key, building);
                            foreach (var position in tempPositionsBlocked)
                            {
                                structuresDictionary.Add(position,building);
                            }
                            break;
                        }
                    } else {
                        var building = SpawnPrefab(buildingTypes[i].GetPrefab(), freeSpot.Key, rotation);
                        structuresDictionary.Add(freeSpot.Key, building);
                    }
                    break;
                }

            }
        }
    }

    private bool VerifyIfBuildingFits(int halfSize,Dictionary<Vector3Int, Direction> freeEstateSpots,
        KeyValuePair<Vector3Int, Direction> freeSpot, ref List<Vector3Int> tempPositionsBlocked) {
        Vector3Int direction = Vector3Int.zero;
        if(freeSpot.Value==Direction.Down ||freeSpot.Value== Direction.Up) {
            direction = Vector3Int.right;
        }
        else {
            direction = new Vector3Int(0, 0, 1);
        }
        for (int i = 1; i <= halfSize; i++){
            var pos1 = freeSpot.Key + direction * 1;
            var pos2 = freeSpot.Key + direction * 1;
            if (!freeEstateSpots.ContainsKey(pos1) || !freeEstateSpots.ContainsKey(pos2)){
                return false;
            }
            tempPositionsBlocked.Add(pos1);
            tempPositionsBlocked.Add(pos2);
        }
        return true;
    }

        private GameObject SpawnPrefab(GameObject prefab, Vector3Int position, Quaternion rotation) {
            var newStructure = Instantiate(prefab, position, rotation, transform);
            return newStructure;
        }

        private Dictionary<Vector3Int, Direction> FindFreeSpaceAroundRoad(List<Vector3Int> roadPositions) {
            Dictionary<Vector3Int, Direction> freeSpace = new Dictionary<Vector3Int, Direction>();
            foreach (var position in roadPositions) {
                var neighbourDirections = PlacementHelper.FindNeighbour(position, roadPositions);
                foreach (Direction direction in System.Enum.GetValues(typeof(Direction))) {
                    if (neighbourDirections.Contains(direction) == false) {
                        var newPosition = position + PlacementHelper.GetOffsetFromDirection(direction);
                        if (freeSpace.ContainsKey(newPosition)) {
                            continue;
                        }
                        freeSpace.Add(newPosition, PlacementHelper.GetReverseDirection(direction));
                    }
                }
            }
            return freeSpace;
        }



}