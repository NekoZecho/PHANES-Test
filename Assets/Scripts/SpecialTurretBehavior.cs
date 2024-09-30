using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTurretBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    public float bulletSpeed = 10f;
    [SerializeField]
    float bulletDrop = 5f;
    [SerializeField]
    float fireRate = 0.5f;
    float timer = 0;
    GameObject player;
    [SerializeField]
    float shootRange = 5f;
    float animplaytime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        animplaytime += Time.deltaTime;
        Vector3 shootDir = player.transform.position - transform.position;
        if (timer > fireRate && shootDir.magnitude <= shootRange)
        {
            timer = 0;
            animplaytime = -0.75f;
            shootDir.Normalize();
            float xShoot = shootDir.x;
            float yShoot = shootDir.y;
            GetComponent<Animator>().SetFloat("x", xShoot);
            GetComponent<Animator>().SetFloat("y", yShoot);
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(bullet, bulletDrop);
        }
        if (animplaytime > 0)
        {
            GetComponent<Animator>().SetFloat("x", 0);
            GetComponent<Animator>().SetFloat("y", 0);
        }
    }
}
