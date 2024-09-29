using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRegular : MonoBehaviour
{

    [SerializeField]
    GameObject prefab;
    [SerializeField]
    float bulletSpeed = 10f;
    [SerializeField]
    float bulletDrop = 5f;
    [SerializeField]
    float fireRate = 0.5f;
    float timer = 0;
    GameObject player;
    [SerializeField]
    float shootRange = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 shootDir = player.transform.position - transform.position;
        if (timer > fireRate && shootDir.magnitude <= shootRange)
        {
            timer = 0;
            shootDir.Normalize();
            float xShoot = shootDir.x;
            float yShoot = shootDir.y;
            GetComponent<Animator>().SetFloat("x", xShoot);
            GetComponent<Animator>().SetFloat("y", yShoot);
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
            Destroy(bullet, bulletDrop);
        }
    }
}
