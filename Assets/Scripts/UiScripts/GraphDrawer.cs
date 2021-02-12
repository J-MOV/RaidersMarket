using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphDrawer : MonoBehaviour
{
    public List<float> dataValues;
    private List<Transform> transformList;

    [SerializeField] private Sprite anchorSprite;
    [SerializeField] private LineRender LineDrawer;

    private RectTransform graphContainer;


    private void Awake()
    {
        graphContainer = transform.Find("Container").GetComponent<RectTransform>();

        dataValues = new List<float>() { 10, 50, 350, 160, 300, 220};
        transformList = new List<Transform>();

        AnchorSorter(dataValues);
    }

    private void AnchorSorter(List<float> anchorTargets)
    {
        int count = 0;
        foreach(var anchor in anchorTargets)
        {
            CreateAnchor(new Vector2(100 * count, anchor));
            count++;
        }

        ConnectAnchors(anchorTargets);
    }

    private void ConnectAnchors(List<float> anchorTargets)
    {
        /*for(int i = 1; i < anchorTargets.Count; i++)
        {
            Vector2 currentValue = new Vector2(100 * i, anchorTargets[i]);
            Vector2 previousValue = new Vector2(100 * (i - 1), anchorTargets[i - 1]);

            GameObject anchorConnection = new GameObject("AnchorConnection", typeof(Image));
            anchorConnection.transform.SetParent(graphContainer, false);

            Vector2 angle = (currentValue - previousValue).normalized;
            float distance = Vector2.Distance(currentValue, previousValue);

            RectTransform rect = anchorConnection.GetComponent<RectTransform>();
        }*/

        LineDrawer.ConnectLine(transformList);
    }

    private void CreateAnchor(Vector2 anchorLocation)
    {
        GameObject anchorPoint = new GameObject("AnchorPoint");

        anchorPoint.transform.SetParent(graphContainer, false);
        anchorPoint.transform.localPosition = new Vector3(anchorLocation.x, anchorLocation.y, -10);

        transformList.Add(anchorPoint.transform);
    }
}
