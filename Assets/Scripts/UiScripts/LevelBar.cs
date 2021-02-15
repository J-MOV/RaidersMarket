using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    private Slider targetSlider;
    private Text targetText;

    private void Start()
    {
        targetSlider = transform.GetComponent<Slider>();
        targetText = transform.GetComponent<Text>();
    }

    private void setLevel()
    {
        for(int i = 0; i < AccountLevelHandler.mileStones.Count; i++)
        {
            if(i == AccountLevelHandler.Level())
            {
                targetSlider.maxValue = AccountLevelHandler.mileStones[AccountLevelHandler.Level()];
            }
        }
    }

    private void Update()
    {

    }
}
