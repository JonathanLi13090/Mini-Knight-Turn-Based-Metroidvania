using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FindPath
{
    public Tilemap tilemap;

    public static List<Node> FindEnemyPath(Tilemap tilemap, Vector2 start, Vector2 end)
    {
        Vector2Int gridStart = (Vector2Int)UtilityTilemap.GetGridPos(tilemap, start);
        Vector2Int gridEnd = (Vector2Int)UtilityTilemap.GetGridPos(tilemap, end);
        //Debug.Log("Path start: " + gridStart + " | end: " + gridEnd);

        Queue<Node> frontier = new Queue<Node>();
        List<Node> visited = new List<Node>();
        List<Node> path = new List<Node>();

        bool nodeFound = false;
        Node startNode = new Node(gridStart, null);
        Node endNode = null;

        frontier.Enqueue(startNode);
        int testBreak = 0;
        while(frontier.Count > 0 && testBreak < 1600)
        {
            testBreak += 1;
            Node current = frontier.Dequeue();
            visited.Add(current);
            Vector2Int up = current.pos + new Vector2Int(0, 1);
            Vector2Int down = current.pos + new Vector2Int(0, -1);
            Vector2Int left = current.pos + new Vector2Int(-1, 0);
            Vector2Int right = current.pos + new Vector2Int(1, 0);
            if(!Node.Contains(visited, up) && !Node.Contains(frontier, up) && UtilityTilemap.GetTile(tilemap, up) == null)
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
        }
        if (nodeFound)
        {
            string pathStr = "Path: ";
            do
            {
                pathStr += endNode.pos + " <- ";
                path.Add(endNode);
                endNode = endNode.previous;
            } while (endNode != startNode);
            Debug.Log(pathStr);
        }
        return path;
    }

    //private void checkFrontierNode(Queue<Node> frontier, List<Node> visted)
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

    public static bool operator == (Node n1, Node n2)
    {
        return n1.pos == n2.pos;
    }

    public static bool operator != (Node n1, Node n2)
    {
        return n1.pos != n2.pos;
    }

    public static bool operator == (Node n1, Vector2Int n2)
    {
        return n1.pos == n2;
    }

    public static bool operator != (Node n1, Vector2Int n2)
    {
        return n1.pos != n2;
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