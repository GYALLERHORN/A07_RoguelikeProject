using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {              // floorPositions : ¹Ù´Ú¿¡ ±ò¸° ¸Ê ÁÂÇ¥ ÁýÇÕ
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        foreach (var position in basicWallPositions)
        {
            tilemapVisualizer.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions) // ¹Ù´Ú ¸Ê ÁÂÇ¥¸¶´Ù
        {
            foreach (var direction in directionsList) // »óÇÏÁÂ¿ì 4¹æÇâ ÀüºÎ Ã¼Å©ÇÏ¸é¼­
            {
                var neighbourPosition = position + direction; // ¹Ù´Ú¸Ê ÁÂÇ¥¿¡ ÀÎÁ¢ÇÑ ÁÂÇ¥°¡ - 
                if (!floorPositions.Contains(neighbourPosition)) // ¹Ù´Ú¸Ê ÁÂÇ¥¿¡ Æ÷ÇÔµÇ¾î ÀÖÁö ¾Ê´Ù¸é => ºñ¾îÀÖ´Â ÁÂÇ¥¶ó¸é 
                {
                    wallPositions.Add(neighbourPosition); // ±× ÁÂÇ¥´Â º®À» ¸¸µé ÁÂÇ¥´Ù.
                }
            }
        }
        return wallPositions;
    }
}
