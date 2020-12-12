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

        Queue<Node> frontier = new Queue<Node>();
        List<Node> visited = new List<Node>();

        bool nodeFound = false;
        Node startNode = new Node(gridStart, null);
        Node endNode = null;
        frontier.Enqueue(startNode);
        int testBreak = 0;
        while (frontier.Count > 0)
        {
            //Debug.Log("while");
            testBreak += 1;
            if (testBreak > 100) { Debug.Log("break"); break; }
            Node current = frontier.Dequeue();
            Vector2Int up = current.pos + new Vector2Int(0, 1);
            Vector2Int down = current.pos + new Vector2Int(0, -1);
            Vector2Int left = current.pos + new Vector2Int(-1, 0);
            Vector2Int right = current.pos + new Vector2Int(1, 0);
            Debug.Log(UtilityTilemap.GetTile(tilemap, up));
            if (!Node.Contains(visited, up) && !Node.Contains(frontier, up) && UtilityTilemap.GetTile(tilemap, up) == null)
            {
                if (up == gridEnd)
                {
                    endNode = new Node(up, current);
                    nodeFound = true;
                    break;
                }
                else frontier.Enqueue(new Node(up, current));
                
            }
            if (!Node.Contains(visited, down) && !Node.Contains(frontier, down) && UtilityTilemap.GetTile(tilemap, down) == null)
            {
                if (down == gridEnd)
                {
                    endNode = new Node(down, current);
                    nodeFound = true;
                    break;
                }
                else frontier.Enqueue(new Node(down, current));

            }
            if (!Node.Contains(visited, left) && !Node.Contains(frontier, left) && UtilityTilemap.GetTile(tilemap, left) == null)
            {
                if (left == gridEnd)
                {
                    endNode = new Node(left, current);
                    nodeFound = true;
                    break;
                }
                else frontier.Enqueue(new Node(left, current));

            }
            if (!Node.Contains(visited, right) && !Node.Contains(frontier, right) && UtilityTilemap.GetTile(tilemap, right) == null)
            {
                if (right == gridEnd)
                {
                    endNode = new Node(right, current);
                    nodeFound = true;
                    break;
                }
                else frontier.Enqueue(new Node(right, current));

            }
        }
        if(nodeFound)
        {
            do
            {
                Debug.Log(endNode.pos);
                endNode = endNode.previous;

            } while (endNode != startNode);
        }
    }
}

public class Node
{
    public Vector2Int pos;
    public Node previous;
    public Vector2 worldPos;

    public Node(Vector2Int pos, Node previous)
    {
        this.pos = pos;
        this.previous = previous;
    }

    public static bool operator == (Node n, Vector2Int pos)
    {
        return n.pos == pos;
    }

    public static bool operator !=(Node n, Vector2Int pos)
    {
        return n.pos != pos;
    }

    public static bool operator ==(Node n1, Node n2)
    {
        return n1.pos == n2.pos;
    }

    public static bool operator !=(Node n1, Node n2)
    {
        return n1.pos != n2.pos;
    }

    public static bool Contains(List<Node> list, Vector2Int pos)
    {
        foreach(Node node in list)
        {
            if (node == pos) return true;
        }
        return false;
    }

    public static bool Contains(Queue<Node> queue, Vector2Int pos)
    {
        foreach (Node node in queue)
        {
            if (node == pos) return true;
        }
        return false;
    }
}
