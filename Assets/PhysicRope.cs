using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PhysicRope : MonoBehaviour
{

    [SerializeField] private List<Transform> points;
    [SerializeField] private List<Transform> connectors;
    [SerializeField] private int connectNb;
    [SerializeField, Min(0.01f)] private float size = 0.3f;


    private void Start()
    {
        
    }


    private void Update()
    {

        Transform lastPoint = points[0];

        for (int i = 0; i < connectNb; i++)
        {
            Transform nextPoint = points[i + 1];
            Transform connector = connectors[i].transform;
            connector.position = CountConPos(lastPoint.position, nextPoint.position);
            if (lastPoint.position == nextPoint.position || nextPoint.position == connector.position)
            {
                connector.localScale = Vector3.zero;
            }
            else
            {
                connector.rotation = Quaternion.LookRotation(nextPoint.position - connector.position);
                connector.localScale = CountSizeOfCon(lastPoint.position, nextPoint.position);
            }

            /*
            if (isConnected)
            {
                cableLength += (lastPoint.position - nextPoint.position).magnitude;
            }
            */

            lastPoint = nextPoint;
        }
    }

    private Vector3 CountConPos(Vector3 start, Vector3 end) => (start + end) / 2f;
    private Vector3 CountSizeOfCon(Vector3 start, Vector3 end) => new Vector3(size, size, (start - end).magnitude / 2f);
}
