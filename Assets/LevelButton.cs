using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public int level;
    public void LoadDungeon()
    {
        FindObjectOfType<LevelManager>().StartLevel(level);
    }
}
