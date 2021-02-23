using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigate : MonoBehaviour
{
    public GameObject targetActive;
    public GameObject targetUnactive;

    public void ChangeTo()
    {
        targetActive.SetActive(true);
    }

    public void TurnOff()
    {
        targetUnactive.SetActive(false);
    }
}
