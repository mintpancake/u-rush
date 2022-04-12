using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool followX = false;

    private Transform player;
    private Vector3 tempPos;
    private float offsetZ = -13.0f;
    // Start is called before the first frame update
    void Start()
    {
        // transform.position = new Vector3(0.0f, 17.35f, -7f);
        if (GameObject.FindWithTag("Player"))
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player)
        {
            if (GameObject.FindWithTag("Player"))
            {
                player = GameObject.FindWithTag("Player").transform;
            }
            return;
        }
        tempPos = transform.position;
        if (followX)
        {
            tempPos.x = player.position.x;
        }
        tempPos.z = player.position.z + offsetZ;
        transform.position = tempPos;
    }
}
