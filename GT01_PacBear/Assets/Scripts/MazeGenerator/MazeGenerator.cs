using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeGenerator
{

    private static int[,] grid;

    private static IntVector2[] directions = new IntVector2[]
    {
        IntVector2.backward,
        IntVector2.left,
        IntVector2.forward,
        IntVector2.right
    };

    public static int[,] Generate(IntVector2 size)
    {
        //Because every other element in our grid is potentially a wall, the grid is larger than the provided size
        grid = new int[size.x * 2 + 1, size.y * 2 + 1];

        //Start with walls everywhere
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = 1;
            }
        }

        IntVector2 startPos = new IntVector2(1, 1);
        grid[startPos.x, startPos.y] = 0;

        IntVector2 current = startPos;

        List<IntVector2> neighbors = GetNeighbors(current);

        //The algorithm goes in a random direction to decide it's path
        int r = Random.Range(0, neighbors.Count);
        IntVector2 nextNeighbor = neighbors[r];
        grid[nextNeighbor.x, nextNeighbor.y] = 0;
        IntVector2 inBetween = (current + nextNeighbor) * 0.5f;
        grid[inBetween.x, inBetween.y] = 0;

        return grid;
    }

    private static List<IntVector2> GetNeighbors (IntVector2 current)
    {
        List<IntVector2> neighbors = new List<IntVector2>();

        foreach(IntVector2 dir in directions)
        {
            //We add the direction twice, because our path only uses every other grid position
            IntVector2 neighborPos = current + dir + dir;

            //Check for sides of graph
            if (neighborPos.x >= grid.GetLength(0) || neighborPos.x < 0 ||
                neighborPos.y >= grid.GetLength(1) || neighborPos.y < 0)
            {
                //Continue (to the next iteration), because break would cancel our entire loop
                continue;
            }

            neighbors.Add(neighborPos);
        }

        return neighbors;
    }



}
