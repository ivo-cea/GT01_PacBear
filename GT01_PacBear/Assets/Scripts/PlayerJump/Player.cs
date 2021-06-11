using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 20;

    public LayerMask collisionMask;

    private float raycastDistance = 1.1f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray downRay = new Ray(transform.position, -transform.up);
            RaycastHit hitInfo;
            Debug.Log("Collision Mask: " + Convert.ToString(collisionMask, 2).PadLeft(32, '0'));

            int layerMask = ~(1 << 3);

            Debug.Log("Binary value: " + Convert.ToString(layerMask, 2).PadLeft(32, '0'));
            Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.red, 0.1f);
            if (Physics.Raycast(downRay, out hitInfo, raycastDistance, layerMask))
            {
                //Debug.Log(hitInfo.collider.name);
                Jump();
            }
        }
    }

    void Jump ()
    {
        rb.velocity = new Vector3(0, jumpForce, 0);
    }
}
