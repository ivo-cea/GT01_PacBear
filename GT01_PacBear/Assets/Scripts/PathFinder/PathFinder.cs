using System.Collections.Generic;

public static class PathFinder
{
    private static Tile[,] graph;

    private static Queue<Tile> frontier;

    public static void GenerateGraph (int[,] world, int nonWalkableIndex)
    {
        //Create a graph that is the same size as the world
        graph = new Tile[world.GetLength(0), world.GetLength(1)];

        for (int i = 0; i < graph.GetLength(0); i++)
        {
            for (int j = 0; j < graph.GetLength(1); j++)
            {
                Tile t = new Tile();
                //We store the position BOTH in the tile AND in the graph
                //This is so we can quickly access the position from the tile afterwards
                t.pos = new IntVector2(i, j);

                graph[i, j] = t;

                t.isWalkable = world[i, j] != nonWalkableIndex;
            }
        }
    }
    
    public static Stack<Tile> GetPath (IntVector2 start, IntVector2 end)
    {
        //Before we calculate a new path, we reset all previousTiles (from the previous path)
        foreach(Tile t in graph)
        {
            t.previousTile = null;
        }

        Tile startTile = graph[start.x, start.y];
        Tile endTile = graph[end.x, end.y];

        frontier = new Queue<Tile>();

        frontier.Enqueue(startTile);

        //Keep searching as long as there is a frontier
        while (frontier.Count > 0)
        {
            Tile current = frontier.Dequeue();
            if (current == endTile)
            {
                //Reached destination!
                Stack<Tile> path = new Stack<Tile>();

                //We keep 'backtracking' and push previous tiles onto the stack, until we reached the start
                while (current != startTile)
                {
                    path.Push(current);
                    //Debug.DrawLine(current.pos, current.previousTile.pos, Color.red, 1);
                    current = current.previousTile;                    
                }

                return path;
            }

            SearchNeighbors(current);
        }

        //Unable to reach destination...
        return null;
    }

    /// <summary>
    /// Searches neighbors of a tile in 4 directions
    /// </summary>
    /// <param name="origin"></param>
    private static void SearchNeighbors (Tile origin)
    {
        SearchNeighbor(origin, IntVector2.forward);
        SearchNeighbor(origin, IntVector2.left);
        SearchNeighbor(origin, IntVector2.right);
        SearchNeighbor(origin, IntVector2.backward);
    }

    private static void SearchNeighbor (Tile origin, IntVector2 direction)
    {
        IntVector2 neighborPos = origin.pos + direction;
        
        //Check for sides of graph
        if (neighborPos.x >= graph.GetLength(0) || neighborPos.x < 0 || 
            neighborPos.y >= graph.GetLength(1) || neighborPos.y < 0)
        {
            return;
        }

        Tile neighbor = graph[neighborPos.x, neighborPos.y];

        if (!neighbor.isWalkable)
        {
            return;
        }

        //If the neighbor already has a previous tile, it has already been searched and we ignore it.
        if (neighbor.previousTile != null)
        {
            return;
        }

        //Debug.DrawLine(origin.pos, neighbor.pos, Color.green, 1);
        neighbor.previousTile = origin;
        //If the neighbor tile is walkable, it becomes part of our frontier
        frontier.Enqueue(neighbor);
    }
}
