using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphDrawer : MonoBehaviour
{
    private RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("Container").GetComponent<RectTransform>();
    }

    private void CreatePoint(Vector2 pointLocation)
    {

    }
}
