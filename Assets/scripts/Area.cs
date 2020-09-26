using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public MapSO TheMap;
    public static PortalSO TargetPortal;
    public GameObject PlayerPrefab;
    public Transform DefaultSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        if (TargetPortal)
        {
            Portal[] portals = FindObjectsOfType<Portal>();
            foreach(Portal portal in portals)
            {
                if(portal.MyPortalSO == TargetPortal)
                {
                    Instantiate(PlayerPrefab, portal.Location, Quaternion.identity);
                }
            }
        }
        else
        {
            Instantiate(PlayerPrefab, DefaultSpawnPoint.position, Quaternion.identity);
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
}
