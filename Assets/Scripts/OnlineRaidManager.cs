using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostRaidInfo {
    public int earnedGold;
    public int lvl;
    public bool completed;
    public Item[] earnedLoot;
}

public class OnlineRaidManager : MonoBehaviour
{

    


    public InspectManager im;

    public PlayerHealth playerHealth;
    public PlayerCombat playerDamage;

    public OnlineConnection connection;
    public LevelManager levelManager;
    public RotateInspectedItem rotate;
    public GameManager gm;
    public ItemRenderer renderer;

    public PlayerStatsSO baseStats;

    public Transform mainMenu;
    public Transform ingamePlayer;
    public Transform EndRaidFailed;
    public Text endRaidFailedTitle;
    public Text endRaidFailedBody;

    public Text raidButtonText;
    public Text raidCompletedTitle;
    public Transform EndRaidScreenCompleted;
    public Transform earnedLootList;
    public GameObject earnedLootSlotPrefab;
    public Text goldEarnedText;


    [HideInInspector] public int raidLvl = -1;
    [HideInInspector] public int maxRaidLvl;

    // Animated the gold earned after raid
    int goldEarned = 0;
    int goldDispalyed = 0;

    public void OnRaidGranted() {
        levelManager.StartLevel(raidLvl);
        mainMenu.gameObject.SetActive(false);
        ingamePlayer.gameObject.SetActive(true);

        playerDamage.baseDamage = baseStats.baseDamage + connection.user.dmg;
        playerHealth.maxHealth = baseStats.maxHealth + connection.user.hp;
        
        playerHealth.HealPlayerToFull();

        rotate.inGame = true;
        rotate.inMainMenu = false;
    }

    public void EndRaid(bool completed) {
        connection.Send("raid_ended", JsonConvert.SerializeObject(completed));
    }

    public void KilledEnemy() {
        connection.Send("killed_enemy");
    }

    public void BackToMenu() {
        mainMenu.gameObject.SetActive(true);
        EndRaidScreenCompleted.gameObject.SetActive(false);
        ingamePlayer.gameObject.SetActive(false);
        EndRaidFailed.gameObject.SetActive(false);
        UpdateRaidButtonText();
    }

    public void OnRaidEnd(string json) {
        
        PostRaidInfo postRaid = JsonConvert.DeserializeObject<PostRaidInfo>(json);

        if (postRaid.completed) {
            EndRaidScreenCompleted.gameObject.SetActive(true);

            while (earnedLootList.childCount > 0) {
                DestroyImmediate(earnedLootList.GetChild(0).gameObject);
            }

            foreach (Item item in postRaid.earnedLoot) {
                IndexedItem origin = connection.GetIndexedItem(item.item);

                Transform earnedLootSlot = Instantiate(earnedLootSlotPrefab, earnedLootList).transform;

                earnedLootSlot.Find("ItemName").GetComponent<Text>().text = origin.name;

                Text rarityText = earnedLootSlot.Find("ItemRarity").GetComponent<Text>();

                rarityText.text = origin.rarity.title.ToUpper();
                rarityText.color = origin.rarity.color;

                raidCompletedTitle.text = "Raid Lvl " + postRaid.lvl + " completed!";

                earnedLootSlot.GetComponent<Button>().onClick.AddListener(() => {
                    im.Inspect(item, true);
                });

                StartCoroutine(renderer.RenderItem(item, earnedLootSlot.Find("Thumbnail").GetComponent<RawImage>()));
            }

            goldDispalyed = 0;
            goldEarned = postRaid.earnedGold;
        } else {
            EndRaidFailed.gameObject.SetActive(true);
            endRaidFailedTitle.text = "Raid Lvl " + postRaid.lvl + " failed";

            string[] failMessages = new string[] {"Oop.", "Better luck next time.", "You got this", "Try again", "I belive in you", "Third time's the charm", "Keep trying..", "Don't give up!" };

            endRaidFailedBody.text = "You took too much damage and died.\n" + failMessages[Random.Range(0, failMessages.Length - 1)];
        }
        
    }

    public void OnLootDrop(int rarity) {
        Debug.Log("Got item drop from server, rarity: " + rarity);
        ItemRarity itemRarity = (ItemRarity) rarity;
        gm.DropLoot(itemRarity);

        if (gm.isEnding()) {
            gm.EndDungeon(true);
        }
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

            raidButtonText.text = "START RAID LVL " + raidLvl;
        }
    }

 
    void Update()
    {
     
        if(goldDispalyed < goldEarned) {
            goldDispalyed++;
            goldEarnedText.text = goldDispalyed.ToString();
        }
    }
}
