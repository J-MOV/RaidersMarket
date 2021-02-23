using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineRaidManager : MonoBehaviour
{

    public OnlineConnection connection;
    public LevelManager levelManager;
    public RotateInspectedItem rotate;

    public Transform mainMenu;
    public Transform ingamePlayer;

    public Text raidButtonText;

    [HideInInspector] public int raidLvl = -1;
    [HideInInspector] public int maxRaidLvl;

    void Start()
    {
        
    }

    public void OnRaidGranted() {
        levelManager.StartLevel(raidLvl);
        mainMenu.gameObject.SetActive(false);
        ingamePlayer.gameObject.SetActive(true);
        rotate.inGame = true;
        rotate.inMainMenu = false;
    }

    public void KilledEnemy() {
        connection.Send("killed_enemy");
    }

    public void StartRaid() {
        connection.Send("start_raid", raidLvl.ToString());
    }

    public void NextLvl() {
        ChangeRaidLevel(1);
    }
    public void PrevLevl() {
        ChangeRaidLevel(-1);
    }

    public void ChangeRaidLevel(int change) {
        raidLvl += change;
        if (raidLvl > maxRaidLvl) raidLvl = maxRaidLvl;
        else if (raidLvl < 1) raidLvl = 1;
        UpdateRaidButtonText();
    }

    public void UpdateRaidButtonText() {
        if(connection.user != null) {
            
            maxRaidLvl = connection.user.lvl;
            if(raidLvl == -1) {
                raidLvl = maxRaidLvl;
            }

            raidButtonText.text = "START RAID LVL: " + raidLvl;
        }
    }

 
    void Update()
    {
        
    }
}
