using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyAI : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    float chaseSpeed = 10f;
    [SerializeField]
    float chaseTriggerDistance = 5.0f;
    [SerializeField]
    Boolean returnHome = true;
    [SerializeField]
    float hesitate = 2f;
    float hesitateTimer = 0;
    Vector3 home;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        home = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if the player gets too close
        Vector3 playerPosition = player.transform.position;
        Vector3 chaseDir = playerPosition - transform.position;
        Vector3 homeDir = home - transform.position;
        if (chaseDir.magnitude < chaseTriggerDistance)
        {
            //chase the player
            //chase direction = players position - my current position
            //move in the direction of the player
            chaseDir.Normalize();
            GetComponent<Rigidbody2D>().velocity = chaseDir * chaseSpeed;
            hesitateTimer = 0;
        }
        else if(returnHome && homeDir.magnitude > 0.2f)
        {
            hesitateTimer += Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (hesitateTimer > hesitate)
            {
                //return home
                homeDir.Normalize();
                GetComponent<Rigidbody2D>().velocity = homeDir * chaseSpeed;
            }

        }
        else
        {
            //if the player is NOT close & and we're not trying to return home, stop moving
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }


    }
}
