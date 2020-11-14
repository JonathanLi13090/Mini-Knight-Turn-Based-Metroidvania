using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public MapSO TheMap;
    public static PortalSO TargetPortal;
    public GameObject PlayerPrefab;
    public Transform DefaultSpawnPoint;
    private Vector2 current_checkpoint;
    private GameObject player;

    public List<GameObject> SpawnObjects = new List<GameObject>();
    public string[] SpawnObjectTags = {"Enemy", "moving_platform"};

    // Start is called before the first frame update
    void Start()
    {
        current_checkpoint = DefaultSpawnPoint.position;

        //findSpawnObjects();

        if (TargetPortal)
        {
            Portal[] portals = FindObjectsOfType<Portal>();
            foreach(Portal portal in portals)
            {
                if(portal.MyPortalSO = TargetPortal)
                {
                    player = Instantiate(PlayerPrefab, portal.Location, Quaternion.identity);
                    current_checkpoint = DefaultSpawnPoint.position;
                }
            }
        }
        else
        {
            player = Instantiate(PlayerPrefab, DefaultSpawnPoint.position, Quaternion.identity);
            current_checkpoint = DefaultSpawnPoint.position;
        }
    }

    void findSpawnObjects()
    {
        foreach(string tag in SpawnObjectTags)
        {
            GameObject[] SpawnObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach(GameObject spawnObject in SpawnObjects)
            {
                SpawnObjects.Add(spawnObject); 
            }
        }
        enemy_controller[] enemyObjects = FindObjectsOfType<enemy_controller>();
        foreach(enemy_controller enemy in enemyObjects)
        {
            SpawnObjects.Add(enemy.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (SpawnObjects.Count <= 0) findSpawnObjects();
    }


    public void ScreenRangeChanged(camera_controller.ScreenRange newScreenRange)
    {
        if (SpawnObjects.Count <= 0)
        {
            findSpawnObjects();
        } 
        foreach(GameObject spawnObject in SpawnObjects)
        {
            if (!spawnObject) continue;
            if(spawnObject.transform.position.x >= newScreenRange.lowerPoint.x && spawnObject.transform.position.x <= newScreenRange.upperPoint.x 
                && spawnObject.transform.position.y >= newScreenRange.lowerPoint.y && spawnObject.transform.position.y <= newScreenRange.upperPoint.y)
            {
                spawnObject.SetActive(true);
            }
            else
            {
                spawnObject.SetActive(false);
            }
        }
    }

    public void OpenPortal(PortalSO portal)
    {
        TargetPortal = TheMap.GetPortalSO(portal);
        if (TargetPortal)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(TargetPortal.MyArea.SceneName);
        }
    }

    public void SetCheckpoint(Transform checkpoint)
    {
       SetCheckpoint(checkpoint.position);
    }

    public void SetCheckpoint(Vector3 checkpointPos)
    {
        current_checkpoint = checkpointPos;
    }

    public void RespawnPlayer()
    {
        player.transform.position = current_checkpoint;
    }
}
