using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;

public class TransformInfo{
    public Vector3 position;
    public Quaternion rotation;
}


public class LSystemScript : MonoBehaviour{
    [SerializeField] private float iterations = 4;
    [SerializeField] private GameObject branch;
    [SerializeField] private float length=10;
    [SerializeField] private float angle = 30;
    private const string axiom = "X";

    private Stack<TransformInfo> transformStack;
    private Dictionary<char, string> rules;
    private string currentString = string.Empty;
    void Start(){
        transformStack = new Stack<TransformInfo>();

        rules = new Dictionary<char, string> {
            { 'X',"[F-[[X]+X]+F[+FX]-X]" },
            { 'F',"FF"}
        };

        Generate();
        
    }

    private void Generate() {
        currentString = axiom;
        StringBuilder sb = new StringBuilder();

        for(int i=0; i < iterations; i++) {
            //loop through current string and create a new string based on the rules
            foreach (char c in currentString){
                sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
            }

            //Set currentString to the new string we just generated.
            currentString = sb.ToString();
            sb = new StringBuilder();
        }

        


        foreach (char c in currentString) { 
            switch(c){
                case 'F':
                    //Draw a straight line
                    Vector3 initialPosition = transform.position;
                    transform.Translate(Vector3.up * length);

                    GameObject treeSegment = Instantiate(branch);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    break;

                case 'X':
                    //does nothing, generate more Fs
                    break;

                case '+':
                    //Rotates clockwise
                    transform.Rotate(Vector3.back * angle);
                    break;

                case '-':
                    //Rotates counter-clockwise
                    transform.Rotate(Vector3.forward * angle);
                    break;

                case '[':
                    //Save current transform info
                    transformStack.Push(new TransformInfo(){
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;

                case ']':
                    //Return to our previously saved transform info
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;

                default:
                    throw new InvalidOperationException("Invalid L-tree operation");
            }
        }
    }
}
