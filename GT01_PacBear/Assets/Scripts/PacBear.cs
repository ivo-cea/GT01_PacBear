using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacBear : BaseUnit
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = new IntVector2(0, -1);            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = IntVector2.forward;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = new IntVector2(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = new IntVector2(1, 0);
        }

        Move();
    }

   
}
