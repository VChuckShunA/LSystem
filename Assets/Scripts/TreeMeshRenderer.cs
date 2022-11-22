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

    public void GenerateMesh()
    {
        gos = GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject g in gos)
        {
            Instantiate(_tubeRenderer,g.transform.position,Quaternion.Euler(g.transform.rotation.x, g.transform.rotation.y,g.transform.rotation.z));
            //tr.transform.position = g.transform.position;
            //tr.transform.Rotate(new Vector3(g.transform.rotation.x,g.transform.rotation.y, g.transform.rotation.z));
            Debug.Log(g.transform.position);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
