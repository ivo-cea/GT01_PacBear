using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BaseObject[] objectPrefabs;

    //0 = pill, 1 = wall, 2 = pacbear    
    private static int[,] grid = new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1},
        {1,0,1,1,1,0,1,0,1,1,1,1,0,1,0,1,1,0,1,0,1},
        {1,0,1,4,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1},
        {1,0,1,0,1,1,0,1,0,1,1,0,1,1,0,1,1,0,1,0,1},
        {1,0,1,0,0,0,0,1,0,0,2,0,0,0,0,0,4,0,1,0,1},
        {1,0,0,0,1,1,0,1,0,1,0,0,1,1,1,1,1,0,0,0,1},
        {1,0,1,0,0,0,0,1,1,1,0,1,1,1,0,1,1,0,1,0,1},
        {1,0,1,0,1,1,1,1,0,4,0,1,0,0,0,1,0,0,1,0,1},
        {1,0,1,0,0,0,1,0,0,1,0,0,0,1,0,0,0,1,1,0,1},
        {1,0,1,1,1,0,0,0,1,1,1,1,0,1,0,1,1,1,1,0,1},
        {1,3,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,3,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
    };
    
   // private static int[,] grid;

    // Start is called before the first frame update
    void Start()
    {
      //  grid = MazeGenerator.Generate(new IntVector2(10, 10));

        PathFinder.GenerateGraph(grid, 1);

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                int objectIndex = grid[i, j];

                BaseObject objectPrefab = objectPrefabs[objectIndex];
                BaseObject baseObject = Instantiate(objectPrefab);
                baseObject.transform.position = new Vector3(i, 0, j);
                baseObject.posInGrid = new IntVector2(i, j);
            }
        }
    }

    public static bool HasWall(IntVector2 pos)
    {
        return grid[pos.x, pos.y] == 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
