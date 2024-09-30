using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSeaking : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    float chaseTriggerDistance = 10.0f;
    [SerializeField]
    float bulletSpeed = 6f;
    float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.up = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 playerPosition = player.transform.position;
        Vector3 chaseDir = playerPosition - transform.position;
        if (chaseDir.magnitude < chaseTriggerDistance  && timer < 5f)
        {
            chaseDir.Normalize();
            transform.up = player.transform.position - transform.position;
            GetComponent<Rigidbody2D>().velocity = chaseDir * bulletSpeed;
        }
    }
}
