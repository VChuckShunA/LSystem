using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [RequireComponent(typeof(Animator))]
    public class AiAgent : MonoBehaviour
    {
        public event Action OnDeath;

        Animator animator;
        public float speed = 0.2f;
        public float rotationSpeed = 10f;

        List<Vector3> pathToGo = new List<Vector3>();
        bool moveFlag = false;
        int indexu = 0;
        Vector3 endPosition;

        public void Initialize(List<Vector3> path){
            pathToGo = path;
            Debug.Log("path:"+path);
            indexu = 1;
            moveFlag = true;
            endPosition = pathToGo[indexu]; //move from current position
            animator = GetComponent<Animator>();
            animator.SetTrigger("Walk");

        }

        private void Update(){
            if (moveFlag){
                PerformMovement();
            }
        }

        private void PerformMovement(){
            if(pathToGo.Count>= indexu){
                float distanceToGo = MoveTheAgent();
                Debug.Log("Distance to go"+distanceToGo);
                if(distanceToGo < 0.05f) {
                    indexu++;
                    Debug.Log("Index is increasing");
                    if(indexu >= pathToGo.Count){
                        moveFlag = false;
                        Destroy(gameObject);
                        return;
                    }
                    endPosition = pathToGo[indexu];
                }
                
                Debug.Log("Didn't Increase the index");
            }
        }

        private float MoveTheAgent(){
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);

            var lookDirection = endPosition - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotationSpeed);
            Debug.Log("Moved the agent");
            
            Debug.Log("Transform.position:"+ transform.position);
            Debug.Log("End Position:" + endPosition);
            return Vector3.Distance(transform.position, endPosition);
        }

        private void OnDestroy(){
            OnDeath?.Invoke();
        }
    }
