using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : BaseObject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PacBear>())
        {
            Destroy(this.gameObject);
        }
    }
}
