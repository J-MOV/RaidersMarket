using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReSwipeController : MonoBehaviour
{
    public GameObject swipeBar;

    private List<Transform> listOfButtons;

    float buttonDistance;
    //float offset = 0;
    private void Start()
    {
        listOfButtons = new List<Transform>();

        buttonDistance += GetComponent<HorizontalLayoutGroup>().spacing;
        buttonDistance += gameObject.transform.GetChild(0).GetComponent<RectTransform>().rect.width;

        for(int i = 0; i < transform.childCount; i++)
        {
            listOfButtons.Add(gameObject.transform.GetChild(i));
        }
        Debug.Log(listOfButtons.Count);

        
    }

    void Update()
    {
        SnapToPosition();
        ExpandCurrent();
    }

    void SnapToPosition()
    {
        float fullDistance = buttonDistance * listOfButtons.Count;

    }
    void ExpandCurrent()
    {

    }
}
