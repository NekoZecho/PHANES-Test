using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDir : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.up = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
