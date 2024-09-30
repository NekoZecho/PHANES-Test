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
    float adrenalRushCharges = 2f;
    float maxAdrenalCharges;
    [SerializeField]
    float adrenalRushCooldown = 5f;
    [SerializeField]
    float adrenalRushHealth = 10f;
    [SerializeField]
    float adrenalRushDuration = 8f;
    float adrenalRushDTimer;
    [SerializeField]
    public float adrenalRushTimeSpeed = 0.25f;
    float adrenalRushCDTimer;
    [SerializeField]
    public bool adrenalRushActive = false;
    [SerializeField]
    bool Invincible = false;


    void Start()
    {
        maxHealth = health;
        maxAdrenalCharges = adrenalRushCharges;
        healthBar.fillAmount = health / maxHealth;
        adrenalRush.fillAmount = adrenalRushCharges / maxAdrenalCharges;
        adrenalineOverlay.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (adrenalRushActive == true)
        {
            adrenalRushDTimer += Time.deltaTime * (1 / adrenalRushTimeSpeed);
            healthTimer += Time.deltaTime * (1 / adrenalRushTimeSpeed);
            regenTimer += Time.deltaTime * (1 / adrenalRushTimeSpeed);
            if (adrenalRushDTimer > adrenalRushDuration)
            {
                AdrenalRushDeactivate();
            }
            
        }
        else
        {
            regenTimer += Time.deltaTime;
            adrenalRushCDTimer += Time.deltaTime;
            adrenalRushDTimer += Time.deltaTime;
        }
        
        if (regenTimer > regenSpeed)
        {
            regenTimer = 0;
            health += regenAmount;
            healthBar.fillAmount = health / maxHealth;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        //We want to take damage IF the player hits the enemy capsule
        //bool key = true;
        if (collision.gameObject.tag == "Enemy" && healthTimer > healthDrain && Invincible == false)
        {
            //health = health - 1;
            health -= 1;
            healthBar.fillAmount = health / maxHealth;
            healthTimer = 0;
            //consequences for taking too much damage
            //IF we take enough damage to bring health to 0, reload level
            if (health <= 0 && adrenalRushCharges >= 1 && adrenalRushCDTimer > adrenalRushCooldown && adrenalRushActive == false)
            {
                AdrenalRush();
            }
            else if (health <= 0 && adrenalRushCDTimer < adrenalRushCooldown)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && healthTimer > healthDrain && Invincible == false)
        {
            health -= 1;
            healthBar.fillAmount = health / maxHealth;
            healthTimer = 0;
            if (health <= 0 && adrenalRushCharges >= 1 && adrenalRushCDTimer > adrenalRushCooldown && adrenalRushActive == false)
            {
                AdrenalRush();
            }
            else if (health <= 0 && adrenalRushCDTimer < adrenalRushCooldown)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" && Invincible == false)
        {
            health -= 2;
            healthBar.fillAmount = health / maxHealth;
            if (health <= 0 && adrenalRushCharges >= 1 && adrenalRushCDTimer > adrenalRushCooldown && adrenalRushActive == false)
            {
                AdrenalRush();
            }
            else if (health <= 0 && adrenalRushCDTimer < adrenalRushCooldown)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
        else if (collision.gameObject.tag == "HeatSeeker" && Invincible == false)
        {
            health -= 15;
            healthBar.fillAmount = health / maxHealth;
            if (health <= 0 && adrenalRushCharges >= 1 && adrenalRushCDTimer > adrenalRushCooldown && adrenalRushActive == false)
            {
                AdrenalRush();
            }
            else if (health <= 0 && adrenalRushCDTimer < adrenalRushCooldown)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fire" && Invincible == false)
        {
            health -= 1;
            healthBar.fillAmount = health / maxHealth;
            if (health <= 0 && adrenalRushCharges >= 1 && adrenalRushCDTimer > adrenalRushCooldown && adrenalRushActive == false)
            {
                AdrenalRush();
            }
            else if (health <= 0 && adrenalRushCDTimer < adrenalRushCooldown)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }

    public void AdrenalRush()
    {
        adrenalRushActive = true;
        Invincible = true;
        health = adrenalRushHealth;
        adrenalRushDTimer = 0;
        Time.timeScale = adrenalRushTimeSpeed;
        healthTimer = 0;
        adrenalineOverlay.GetComponent<Image>().enabled = true;
    }

    public void AdrenalRushDeactivate()
    {
        adrenalRushActive = false;
        Invincible = false;
        Time.timeScale = 1;
        adrenalineOverlay.GetComponent<Image>().enabled = false;
        adrenalRushCharges -= 1;
        adrenalRush.fillAmount = adrenalRushCharges / maxAdrenalCharges;
        adrenalRushCDTimer = 0;

    }
}
