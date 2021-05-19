using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : BaseObject
{
    public float speed = 1;

    protected IntVector2 direction;
    private IntVector2 nextPosInGrid;
    private IntVector2 prevDirection;

    protected float moveTimer;

    private void Start()
    {
        //So it doesn't move to 0,0 at the start
        nextPosInGrid = posInGrid;
    }

    protected void Move()
    {
        //Converting from IntVector to Unity Vector (for position)
        Vector3 currentPos = posInGrid;
        Vector3 nextPos = nextPosInGrid;

        //Will always be between 0 and 1
        moveTimer += Time.deltaTime * speed;

        transform.position = Vector3.Lerp(currentPos, nextPos, moveTimer);

        if (moveTimer >= 1)
        {
            posInGrid = nextPosInGrid;
            moveTimer = 0;

            nextPosInGrid = posInGrid + direction;

            if (GameManager.HasWall(nextPosInGrid))
            {
                //We check if the previous direction guides us into a wall
                nextPosInGrid = posInGrid + prevDirection;
                if (GameManager.HasWall(nextPosInGrid))
                {
                    //Stop.
                    nextPosInGrid = posInGrid;
                }
            }
            else
            {
                if (direction != IntVector2.zero)
                {
                    //Rotate towards the direction it's going
                    transform.forward = direction;
                }
                
                prevDirection = direction;
            }
        }
    }

}
