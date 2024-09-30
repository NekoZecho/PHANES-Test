using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDestruction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            if (collision.gameObject.tag != "EnemyBullet")
            {
                Destroy(gameObject);
            }
        }
    }
}
