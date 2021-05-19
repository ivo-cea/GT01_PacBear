using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Vector3 offset = new Vector3(0,15,-15);

    private PacBear pacBear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pacBear)
        {
            pacBear = FindObjectOfType<PacBear>();
        }

        if (pacBear)
        {
            this.transform.position = pacBear.transform.position + offset;
        }
    }
}
