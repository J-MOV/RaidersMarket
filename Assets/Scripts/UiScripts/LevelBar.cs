using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    [SerializeField] private Slider targetSlider;
    [SerializeField] private Text targetText;

    private void Start()
    {
        //targetSlider = transform.GetComponent<Slider>();
        //targetText = transform.GetComponent<Text>();

        List<int> temp = new List<int>();

        for(int i = 0; i < 5; i++) // Reckon the server will handle this data later
        {
            temp.Add(100 * (i * i)); // 5: 0, 100, 400, 900, 1600;
        }

        AccountLevelHandler.Initzialize(temp);

        setLevel();
    }

    private void setLevel()
    {
        //Calculate the ammount of Experience left to the next level
        targetSlider.maxValue = AccountLevelHandler.mileStones[AccountLevelHandler.Level() + 1];
        targetSlider.minValue = 0;
        //Display the Level
        targetText.text = "Level: " + AccountLevelHandler.Level();
        //Display the Experience
        targetSlider.value = (float)AccountLevelHandler.Experience();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            AccountLevelHandler.AddExperience(10);
            Debug.Log("Exp: " + AccountLevelHandler.Experience());
            Debug.Log("Level " + AccountLevelHandler.Level());

            setLevel();
        }
    }
}
