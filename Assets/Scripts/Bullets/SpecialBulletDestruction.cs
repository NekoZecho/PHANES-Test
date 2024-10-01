using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemyBulletDestruction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            if (collision.gameObject.tag != "EnemyBullet")
            {
                if (collision.gameObject.tag != "HeatSeeker")
                {
                    if (collision.gameObject.tag != "PlayerBullet")
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
