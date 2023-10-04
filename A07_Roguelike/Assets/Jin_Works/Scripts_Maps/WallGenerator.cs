using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {              // floorPositions : ¹Ù´Ú¿¡ ±ò¸° ¸Ê ÁÂÇ¥ ÁýÇÕ
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        CreateBasicWall(tilemapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions,
        HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, 
        HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
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
