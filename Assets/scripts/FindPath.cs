using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FindPath : MonoBehaviour
{
    public Tilemap tilemap;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemyPath(tilemap, transform.position, GameObject.FindGameObjectWithTag("player").transform.position);
    }

    private void FindEnemyPath(Tilemap tilemap, Vector2 start, Vector2 end)
    {
        Vector2Int gridStart = (Vector2Int) UtilityTilemap.GetGridPos(tilemap, start);
        Vector2Int gridEnd = (Vector2Int)UtilityTilemap.GetGridPos(tilemap, end);
        Debug.Log("Path start: " + gridStart + " | end: " + gridEnd);

        List<Node> frontier = new List<Node>();
        List<Node> visited = new List<Node>();

        frontier.Add(new Node(gridStart));
        while(frontier.Count > 0)
        {

        }
    }
}

public class Node
{
    public Vector2Int pos;
    public Node previous;

    public Node(Vector2Int pos)
    {
        this.pos = pos;
    }
}
