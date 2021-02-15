using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject optionsPanel;

    [SerializeField] string sceneName;

    int amountOfRestarts;

    float waitTillAnimationFinished = 3f;

    Text goldText;
    Text energyText;

    public Canvas levelSelectorPanel;


    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "MehmetScene")
        {
            goldText = GameObject.Find("GoldText").GetComponent<Text>();

            energyText = GameObject.Find("EnergyText").GetComponent<Text>();

            goldText.text = "GOLD: " + PlayerPrefs.GetInt("amountOfGoldPlayerHas");

            energyText.text = "ENERGY: " + PlayerPrefs.GetInt("currentEnergy");
        }

        amountOfRestarts = PlayerPrefs.GetInt("TotalRestarts");
    }

    public void MarketPlaceOpened()
    {
        //TODO: Play animation for closing the menu
        StartCoroutine(RemovePanel());
        SceneManager.LoadSceneAsync(sceneName);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        Debug.Log("I loaded the Marketplace");
        mainPanel.SetActive(true);
    }
    public void OptionsMenuOpened()
    {
        StartCoroutine(RemovePanel());
        optionsPanel.SetActive(true);
        Debug.Log("You are now in the options menu");
        mainPanel.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void LoadAnyScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync("MehmetScene");
    }
    public void RestartLevel()
    {
        amountOfRestarts++;
        PlayerPrefs.SetInt("TotalRestarts", amountOfRestarts);
        SceneManager.LoadSceneAsync("ViktorScene");
        AnalyticsResult results = Analytics.CustomEvent("AmountOfRestarts", new Dictionary<string, object>
    {
        {"Total Number Of Restarts: ", amountOfRestarts },
    });
        Debug.Log("Results: " + results);
    }

    IEnumerator RemovePanel()
    {
        //TODO: Play animation for closing the menu
        yield return new WaitForSeconds(waitTillAnimationFinished);
        mainPanel.SetActive(false);
    }


    public void OpenLevelSelector()
    {
        levelSelectorPanel.gameObject.SetActive(true);
    }

    public void CloseLevelSelector()
    {
        levelSelectorPanel.gameObject.SetActive(false);
    }
}
