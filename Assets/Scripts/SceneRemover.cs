using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRemover : MonoBehaviour
{
    private Transform player;
    private float offset = 300f;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Player"))
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            if (GameObject.FindWithTag("Player"))
            {
                player = GameObject.FindWithTag("Player").transform;
            }
            return;
        }
        if(player.position.z - transform.position.z > offset)
        {
            Destroy(gameObject);
        }
    }
}
