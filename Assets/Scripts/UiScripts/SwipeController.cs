using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Change some optimization to reduce the declaration of variables on update and for loops.
//Like a check or a variable with the current level stored so that the program doesn’t loop through so much. 
//Could save processing power (and battery life for mobile)

public class SwipeController : MonoBehaviour
{
    public GameObject swipeBar;

    float swipePosition = 0; // <-- Where I am right now
    float divideDistance;

    float[] setPosition;

    private void Start()
    {
        setPosition = new float[transform.childCount];
        divideDistance = 1f / (setPosition.Length - 1f);
    }

    void Update()
    {
        for(int i = 0; i < setPosition.Length; i++)
        {
            setPosition[i] = divideDistance * i;
        }

        if(Input.GetMouseButton(0))
        {
            swipePosition = swipeBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for(int i = 0; i < setPosition.Length; i++)
            {
                if(swipePosition < setPosition[i] + divideDistance && swipePosition > setPosition[i] - (divideDistance / 2))
                {
                    swipeBar.GetComponent<Scrollbar>().value = Mathf.Lerp(swipeBar.GetComponent<Scrollbar>().value, setPosition[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < setPosition.Length; i++)
        {
            if (swipePosition < setPosition[i] + (divideDistance / 2) && swipePosition > setPosition[i] - (divideDistance / 2))
            {
                //Debug.LogWarning("Current Selected Level" + i);
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);
                for (int j = 0; j < setPosition.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }

    }
}
