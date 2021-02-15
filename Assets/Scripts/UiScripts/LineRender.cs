using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    [SerializeField] private LineRenderer LineRend;
    private List<Transform> pointList;

    private void Awake()
    {        
        //LineRend = gameObject.GetComponent<LineRenderer>();

        pointList = new List<Transform>();
    }

    public void ConnectLine(List<Transform> points)
    {
        Debug.Log(points.Count);

        LineRend.positionCount = points.Count;

        Debug.Log(LineRend.positionCount);

        pointList = points;

        for (int i = 0; i < pointList.Count; i++)
        {
            LineRend.SetPosition(i, pointList[i].position);
        }
    }

    private void Update()
    {
        /*for(int i = 0; i < pointList.Count; i++)
        {
            LineRend.SetPosition(i, pointList[i].position);
        }*/
    }
}
