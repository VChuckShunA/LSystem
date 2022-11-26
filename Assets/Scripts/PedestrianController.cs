using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianController : MonoBehaviour
{
    private Color startcolor=Color.blue;

    public void OnMouseDown()
    {
        CameraController.instance.followTransform = transform;
        startcolor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}
