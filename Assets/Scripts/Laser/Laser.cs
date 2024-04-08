using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Laser : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask mirorMask;
    public float defaultLenght = 20;
    public int numOfReflections = 2;

    private LineRenderer lineRenderer;
    private RaycastHit hit;

    private Ray ray;
    private Vector3 direction;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        LaserReflection();
    }

    private void LaserReflection()
    {
        ray = new Ray(transform.position, transform.forward);

        lineRenderer.positionCount = 1; ;
        lineRenderer.SetPosition(0, transform.position);

        float remainLenght = defaultLenght;

        for (int i = 0; i< numOfReflections; i++)
        {
            if(Physics.Raycast(ray.origin, ray.direction, out hit, remainLenght, mirorMask))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount-1, hit.point);

                remainLenght -= Vector3.Distance(ray.origin, hit.point);

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount-1, ray.origin + (ray.direction * remainLenght));
            }
        }
    }
}
