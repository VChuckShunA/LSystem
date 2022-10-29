using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureHelper : MonoBehaviour
{
    public GameObject prefab;
    public Dictionary<Vector3Int, GameObject> structuresDictionary = new Dictionary<Vector3Int, GameObject>();


    public void PlaceStructuresAroundRoad(List<Vector3Int> roadPositions){
        Dictionary<Vector3Int, Direction> freeEstateSpots = FindFreeSpaceAroundRoad(roadPositions);
        foreach (var freeSpot in freeEstateSpots){
            var rotation = Quaternion.identity;
            switch (freeSpot.Value){
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
            Instantiate(prefab, freeSpot.Key, rotation, transform);
        }
    }

    private Dictionary<Vector3Int, Direction> FindFreeSpaceAroundRoad(List<Vector3Int> roadPositions) {
        Dictionary<Vector3Int, Direction> freeSpace = new Dictionary<Vector3Int, Direction>();
        foreach(var position in roadPositions) {
            var neighbourDirections = PlacementHelper.FindNeighbour(position, roadPositions);
            foreach (Direction direction in System.Enum.GetValues(typeof(Direction))){
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
