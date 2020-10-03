using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObjects/Map")]
public class MapSO : ScriptableObject
{
    public Connection[] Connections;

    public PortalSO GetPortalSO(PortalSO start)
    {
        PortalSO end = null;
        foreach(Connection connection in Connections)
        {
            if(connection.Start == start)
            {
                end = connection.End;
            }
            else if(connection.End == start && !connection.directional)
            {
                end = connection.Start;
            }
        }

        return end;
    }
}

[System.Serializable]
public class Connection
{
    public PortalSO Start;
    public PortalSO End;
    public bool directional;

}
