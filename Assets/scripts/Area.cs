using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public MapSO TheMap;
    public static PortalSO TargetPortal;
    public GameObject PlayerPrefab;
    public Transform DefaultSpawnPoint;
    private Vector2 current_checkpiont;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
        if (TargetPortal)
        {
            Portal[] portals = FindObjectsOfType<Portal>();
            foreach(Portal portal in portals)
            {
                if(portal.MyPortalSO = TargetPortal)
                {
                    player = Instantiate(PlayerPrefab, portal.Location, Quaternion.identity);
                    current_checkpiont = portal.Location;
                }
            }
        }
        else
        {
            player = Instantiate(PlayerPrefab, DefaultSpawnPoint.position, Quaternion.identity);
            current_checkpiont = DefaultSpawnPoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        current_checkpiont = checkpoint.position;
        Debug.Log("Checkpoint Set: " + checkpoint.position);
    }

    public void RespawnPlayer()
    {
        player.transform.position = current_checkpiont;
        
    }
}
