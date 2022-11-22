using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridHelper : MonoBehaviour
{
    public string cellStr;

    public GameObject pM_GO;
    public PlacementManager placementManager;
    // Start is called before the first frame update
    void Start()
    {
        pM_GO = GameObject.FindWithTag("placementManager");
        placementManager = pM_GO.GetComponent<PlacementManager>();
        CellType parsed_enum = (CellType)System.Enum.Parse(typeof(CellType), cellStr);
        /*placementManager.CreateANewStructureModel(new Vector3Int(Mathf.FloorToInt(transform.position.x),
            Mathf.FloorToInt(transform.position.y),
            Mathf.FloorToInt(transform.position.z)), this.gameObject, parsed_enum);*/
        placementManager.AddToGrid(new Vector3Int(Mathf.FloorToInt(transform.position.x),
            Mathf.FloorToInt(transform.position.y),
            Mathf.FloorToInt(transform.position.z)), cellStr);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
