using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Laser : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public Material material;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 4;
        lineRenderer.material = material;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1, transform.position + transform.forward * 5);
        lineRenderer.SetPosition(2, transform.position + Vector3.left  + transform.forward * 8);
        lineRenderer.SetPosition(3, transform.position + Vector3.left * 3 + transform.forward * 10);

    }

    void Update()
    {
        
    }
}
