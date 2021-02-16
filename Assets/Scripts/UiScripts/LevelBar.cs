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
        //Check for how much Experience the Account has
        //Check the level of the account
        //Display the Level
        //Display the Experience
        //Calculate the ammount of Experience left to the next level
    }

    private void Update()
    {

    }
}
