using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : BaseUnit
{
    private IntVector2[] directions =
    {
        IntVector2.forward,
        IntVector2.backward,
        IntVector2.left,
        IntVector2.right
    };

    private PacBear pacBear;

    protected override void Start()
    {
        base.Start();
        pacBear = FindObjectOfType<PacBear>();
    }

    private void Update()
    {
        if (moveTimer == 0)
        {
            //Wonder();
            ChasePlayer();
        }
        Move();
    }

    private void ChasePlayer ()
    {
        Stack<Tile> path = PathFinder.GetPath(nextPosInGrid, pacBear.nextPosInGrid);
        if (path != null && path.Count > 0)
        {
            //The destinationPos is the first part of the path towards the player
            IntVector2 destinationPos = path.Pop().pos;
            //We calculate the direction, since that's what the ghost needs to move
            direction = destinationPos - nextPosInGrid;
        } else
        {
            direction = IntVector2.zero;
        }
        
    }

    private void Wonder()
    {
        //TODO what happens when the ghost is surrounded by 4 walls?

        List<IntVector2> possibleDirections = new List<IntVector2>();
        //Loop through all 4 directions
        foreach (IntVector2 dir in directions)
        {
            //We can only go there if there's not a wall in this direction
            //Also, we can't turn around
            if (!GameManager.HasWall(nextPosInGrid + dir) && dir != -direction)
            {
                possibleDirections.Add(dir);
            }
        }

        //If the Ghost hits a dead end, it HAS TO turn around.
        if (possibleDirections.Count == 0)
        {
            possibleDirections.Add(-direction);
        }

        int r = Random.Range(0, possibleDirections.Count);
        direction = possibleDirections[r];
    }
}
