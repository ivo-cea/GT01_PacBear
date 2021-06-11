using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost : BaseUnit
{
    [SerializeField]
    private SkinnedMeshRenderer ghostRenderer;

    private IntVector2[] directions =
    {
        IntVector2.forward,
        IntVector2.backward,
        IntVector2.left,
        IntVector2.right
    };

    private PacBear pacBear;

    private bool isEdible;
    private bool isAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            ghostRenderer.material.SetFloat("_IsAlive", value ? 1 : 0);
            //this is called EVERY time isAlive is changed.
        }
    }

    private bool _isAlive;

    private IntVector2 startPosInGrid;

    protected override void Start()
    {
        base.Start();
        pacBear = FindObjectOfType<PacBear>();
        startPosInGrid = posInGrid;
    }

    private void OnEnable()
    {
        PacBear.onEatHoney += PacBear_onEatHoney;
    }

    private void OnDisable()
    {
        PacBear.onEatHoney -= PacBear_onEatHoney;
    }

    private void PacBear_onEatHoney()
    {
        isEdible = true;
        ghostRenderer.material.SetFloat("_IsBlinking", 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PacBear>())
        {
            if (isEdible)
            {                
                isAlive = false;
            } else
            {
                //Game restart
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    private void Update()
    {
        if (moveTimer == 0)
        {
            if (isAlive)
            {
                //Wonder();
                ChasePlayer();
            } else
            {
                ReturnToStart();
            }            
        }
        Move();
    }

    private void ReturnToStart()
    {
        Stack<Tile> path = PathFinder.GetPath(nextPosInGrid, startPosInGrid);
        if (path != null && path.Count > 0)
        {
            //The destinationPos is the first part of the path towards the player
            IntVector2 destinationPos = path.Pop().pos;
            //We calculate the direction, since that's what the ghost needs to move
            direction = destinationPos - nextPosInGrid;
        }
        else
        {
            direction = IntVector2.zero;
            isAlive = true;
        }
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
