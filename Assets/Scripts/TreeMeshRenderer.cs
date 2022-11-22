using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMeshRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private TubeRenderer _tubeRenderer;
    [SerializeField] private Mesh mesh;
    private GameObject[] gos;
    void Start()
    {
        
    }

    public void GenerateMesh(Vector3 branchPosition, Vector3 branchRotation)
    {
        //gos = GameObject.FindGameObjectsWithTag("Tree");
        //foreach (GameObject g in gos)
        //{
            
            Instantiate(_tubeRenderer,branchPosition,Quaternion.Euler(branchRotation.x, branchRotation.y,branchRotation.z));
            //tr.transform.position = g.transform.position;
            //tr.transform.Rotate(new Vector3(g.transform.rotation.x,g.transform.rotation.y, g.transform.rotation.z));
            //Debug.Log();
            
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
