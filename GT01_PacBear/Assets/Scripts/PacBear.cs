using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacBear : BaseUnit
{
    public static event Action onEatHoney;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = IntVector2.backward;            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = IntVector2.forward;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = IntVector2.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = IntVector2.right;
        }

        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Honey>())
        {
            Destroy(other.gameObject);
            onEatHoney?.Invoke();
        }
    }


}
