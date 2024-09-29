using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    float health = 20;
    [SerializeField]
    string levelToLoad;
    float maxHealth;
    [SerializeField]
    Image healthBar;
    float healthDrain = 0.5f;
    float healthTimer = 0;
    [SerializeField]
    float regenSpeed = 2f;
    [SerializeField]
    float regenAmount = 2f;
    float regenTimer;
    [SerializeField]
    Image adrenalRush;
    [SerializeField]
    Image adrenalineOverlay;
    [SerializeField]
    float adrenalRushCharges = 3f;
    [SerializeField]
    float adrenalRushCooldown = 5f;
    [SerializeField]
    float adrenalRushHealth = 10f;
    [SerializeField]
    float adrenalRushDuration = 8f;
    float adrenalRushDTimer;
    [SerializeField]
    float adrenalRushTimeSpeed = 0.25f;
    float adrenalRushCDTimer;
    [SerializeField]
    public bool adrenalRushActive = false;


    void Start()
    {
        maxHealth = health;
        healthBar.fillAmount = health / maxHealth;
        adrenalineOverlay.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthTimer += Time.deltaTime;
        regenTimer += Time.deltaTime;
        adrenalRushCDTimer += Time.deltaTime;
        adrenalRushDTimer += Time.deltaTime;
        
        if (regenTimer > regenSpeed)
        {
            regenTimer = 0;
            health += regenAmount;
            healthBar.fillAmount = health / maxHealth;
        }
        if (adrenalRushActive == true)
        {
            adrenalRushDTimer -= Time.deltaTime * 2f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        //We want to take damage IF the player hits the enemy capsule
        //bool key = true;
        if (collision.gameObject.tag == "Enemy" && healthTimer > healthDrain)
        {
            //health = health - 1;
            health -= 1;
            healthBar.fillAmount = health / maxHealth;
            healthTimer = 0;
            //consequences for taking too much damage
            //IF we take enough damage to bring health to 0, reload level
            if (health <= 0 && adrenalRushCharges >= 1 && adrenalRushCDTimer > adrenalRushCooldown)
            {
                health = 1;
                Time.timeScale = adrenalRushTimeSpeed;
            }
            else if (health <= 0 && adrenalRushCharges <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //SceneManager.LoadScene(levelToLoad);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && healthTimer > healthDrain)
        {
            health -= 1;
            healthBar.fillAmount = health / maxHealth;
            healthTimer = 0;
            if ((health <= 0 && adrenalRushCharges <= 0))
            {
                SceneManager.LoadScene(levelToLoad);
                //SceneManager.LoadScene(levelToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            health -= 5;
            healthBar.fillAmount = health / maxHealth;
            if (health <= 0 && adrenalRushCharges <= 0)
            {
                SceneManager.LoadScene(levelToLoad);
                //SceneManager.LoadScene(levelToLoad);
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health -= 1;
            healthBar.fillAmount = health / maxHealth;
            if (health <= 0 && adrenalRushCharges >= 1 && adrenalRushCDTimer > adrenalRushCooldown)
            {
                AdrenalRush();
            }
            else if (health <= 0 && adrenalRushCharges <= 0)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }

    public void AdrenalRush()
    {
        health = adrenalRushHealth;
        Time.timeScale = 0.25f;
        healthTimer = 0;
        adrenalRushActive = true;

    }

}
