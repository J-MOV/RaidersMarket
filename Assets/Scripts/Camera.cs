using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




public class Camera : MonoBehaviour
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

    public GameObject mainDungeon;
    public GameObject oldDungeon;


    // TODO: Delete this
    public bool TEST_CAMERA_CHANGE_VIEW_TO_RAID = false;
   
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

        while(dungeons.Count > 1) {
            Destroy(dungeons[0].gameObject);
            dungeons.RemoveAt(0);
        }
        dungeons.Add(Instantiate(GetRandomDugeonAsset()).transform);
        

        /*DestroyImmediate(oldDungeon);
        oldDungeon = mainDungeon;
        mainDungeon = Instantiate(GetRandomDugeonAsset());
        mainDungeon.transform.position = dungeonPosition + dungeonOffset;*/
    }

    // Changes to the next enemy.
    public void PanNext(GameObject monster = null) {
        AddDungeon();
        if (monster != null) Instantiate(monster, dungeons[1].Find("MonsterSpawn"));
        dungeons[1].position = dungeonPosition + dungeonOffset;
    }

    GameObject GetRandomDugeonAsset() {
        return dungeonAssets[Random.Range(0, dungeonAssets.Count - 1)]; 
    }


    void Start()
    {
        ChangeDesiredPosition(menuPosition);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("p")) PanNext(monsterTest);
/*
        if (TEST_CAMERA_CHANGE_VIEW_TO_RAID) {
            ChangeDesiredPosition(raidPosition);
        } else {
            ChangeDesiredPosition(menuPosition);
        }
*/

        /*mainDungeon.transform.position = 
        oldDungeon.transform.position = mainDungeon.transform.position - dungeonOffset;*/

        if(dungeons.Count > 1) {
            dungeons[1].position = Vector3.SmoothDamp(dungeons[1].transform.position, dungeonPosition, ref dungeonVelocity, dungeonPanSpeed);
            dungeons[0].position = dungeons[1].position - dungeonOffset;
        }
        


        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, cameraSpeed);
    }
}
