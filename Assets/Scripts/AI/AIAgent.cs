using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class AIAgent : MonoBehaviour
{
    public event Action OnDeath;

    Animator animator;
    public float speed = 0.2f;
    public float rotationSpeed = 10f;

    List<Vector3> pathToGo = new List<Vector3>();
    bool moveFlag = false;
    int index = 0;
    Vector3 endPosition;

    //Ininitalize it to make it start moving
    public void Initialize(List<Vector3> path){
        pathToGo = path;
        index = 1; //increase index
        moveFlag = true;
        endPosition = pathToGo[index]; //move to next index
        animator = GetComponent<Animator>();
        animator.SetTrigger("Walk");
    }

    private void Update(){
        if (moveFlag)
        {
            PerformMovement();
        }
    }

    private void PerformMovement(){
        //9if we still have some positions to go through
        if (pathToGo.Count > index)
        {
            float distanceToGo = MoveTheAgent();
            if (distanceToGo < 0.05f)
            {
                index++;
                if (index >= pathToGo.Count)
                {
                    //stop the game object and destroy it cuz it has reached the target location
                    moveFlag = false;
                    Destroy(gameObject);
                    return;
                }
                endPosition = pathToGo[index];
            }
        }
    }

    private float MoveTheAgent(){
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, endPosition, step);

        var lookDirection = endPosition - transform.position;
        //Set the rotation to match the direction our ai agent should look at 
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotationSpeed);
        // return distance between the agent and the end position
        return Vector3.Distance(transform.position, endPosition);
    }

    private void OnDestroy(){
        OnDeath?.Invoke();
    }
}
