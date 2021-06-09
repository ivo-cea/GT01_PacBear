using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTest : MonoBehaviour
{
    public LayerMask dad;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(LayerMask.GetMask("UI"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
