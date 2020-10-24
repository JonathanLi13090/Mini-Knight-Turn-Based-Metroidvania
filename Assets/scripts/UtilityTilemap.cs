using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class UtilityTilemap 
{
    public static TileBase GetTile(Tilemap tilemap, Vector2 worldPos)
    {
        Vector3Int tilePos = tilemap.WorldToCell(worldPos);
        return tilemap.GetTile(tilePos);
    }

    
    public static bool CheckTileType(Tilemap tilemap, Vector2 worldPos, string tileType)
    {
        TileBase tileBase = GetTile(tilemap, worldPos);
        if (tileBase != null && tileBase.name == tileType) return true;
        else return false;
    }

    public static bool CheckTileType(List<Tilemap> tilemaps, Vector2 worldPos, string tileType)
    {
        foreach(Tilemap timemap in tilemaps)
        {
            if (CheckTileType(timemap, worldPos, tileType)) return true;
        }
        return false;
    }

    public static bool CheckTileType(string tilemapTag, Vector2 worldPos, string tileType)
    {
        List<Tilemap> tilemaps = new List<Tilemap>();
        GameObject[] tagObjects = GameObject.FindGameObjectsWithTag(tilemapTag);
        foreach(GameObject tagObject in tagObjects)
        {
            if (tagObject.GetComponent<Tilemap>())
            {
                tilemaps.Add(tagObject.GetComponent<Tilemap>());
            }
        }

        return CheckTileType(tilemaps, worldPos, tileType);
    }
}
