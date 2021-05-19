using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : BaseUnit
{
    private IntVector2[] directions =
    {
        IntVector2.forward,
        new IntVector2(0,-1),
        new IntVector2(1,0),
        new IntVector2(-1,0)
    };

    private void Update()
    {
        if (moveTimer == 0)
        {
            int r = Random.Range(0, directions.Length);
            direction = directions[r];
        }
        Move();
    }
}
