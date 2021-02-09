using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    float waitTillAnimationFinished = 3f;

    public void StartGame()
    {
        StartCoroutine(RemovePanel());
        //TODO: Let the game begin.
    }



    public void MarketPlaceOpened()
    {
        //TODO: Play animation for closing the menu
        StartCoroutine(RemovePanel());
        SceneManager.LoadSceneAsync("Marketplace");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Marketplace"));
    }

    public void OptionsMenuOpened()
    {

    }
    IEnumerator RemovePanel()
    {
        //TODO: Play animation for closing the menu
        yield return new WaitForSeconds(waitTillAnimationFinished);
        mainPanel.SetActive(false);
    }
}
