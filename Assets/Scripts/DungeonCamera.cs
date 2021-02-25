using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




public class DungeonCamera : MonoBehaviour
{

    // TODO: delete this
    public GameObject monsterTest;

    [SerializeField] Vector3 dungeonPosition = new Vector3(0f, 0f, 2.2f);
    [SerializeField] Vector3 dungeonOffset = new Vector3(0f, 0f, -29.91f);

    public List<Transform> dungeons = new List<Transform>();


    public List<GameObject> dungeonAssets;

    bool inRaid = false;

    public Transform menuPosition;
    public Transform raidPosition;

    private Vector3 desiredPosition;
    private Vector3 cameraVelocity = Vector3.zero;
    private float cameraSpeed = .3f;

    private Vector3 dungeonVelocity = Vector3.zero;
    [SerializeField] private float dungeonPanSpeed = .3f;

   
    public void EnterRaid() {
        ChangeDesiredPosition(raidPosition);
    }

    public void ExitRaid() {
        ChangeDesiredPosition(menuPosition);
    }

    void ChangeDesiredPosition(Transform position) {
        //cameraVelocity = Vector3.zero;
        desiredPosition = position.position;
    }

    void AddDungeon() {

        if(dungeons.Count > 1) {
            Destroy(dungeons[0].gameObject);
            dungeons.RemoveAt(0);
        }

        dungeons.Add(Instantiate(GetRandomDugeonAsset()).transform);
    }

    // Changes to the next enemy.
    public void PanNext(GameObject[] monsters) {
        AddDungeon();
        foreach(GameObject monster in monsters) { 
            monster.transform.SetParent(dungeons[1].Find("MonsterSpawn"));
            monster.transform.position = monster.transform.position + dungeons[1].Find("MonsterSpawn").position;
        }
        dungeons[1].position = dungeonPosition + dungeonOffset;
    }

    GameObject GetRandomDugeonAsset() {
        return dungeonAssets[Random.Range(0, dungeonAssets.Count - 1)]; 
    }


    void Start()
    {
        menuPosition = transform;
        ChangeDesiredPosition(menuPosition);
    }

    // Update is called once per frame
    void Update()
    {

        if(dungeons.Count > 1) {
            dungeons[1].position = Vector3.SmoothDamp(dungeons[1].transform.position, dungeonPosition, ref dungeonVelocity, dungeonPanSpeed);
            dungeons[0].position = dungeons[1].position - dungeonOffset;
        }
        
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, cameraSpeed);
    }
}
