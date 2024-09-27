using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 10f;
    [SerializeField]
    float runSpeed = 15f;
    [SerializeField]
    float Stamina = 15f;
    float maxStamina;
    [SerializeField]
    float staminaRegen = 0.5f;
    [SerializeField]
    float staminaRegenSpeed = 1f;
    float staminaRegenTimer;
    [SerializeField]
    float catchBreath = 4f;
    float catchBreathTimer;
    public bool isRunning;
    //[SerializeField]
    //float crouchSpeed = 5f;

    void Start()
    {
        maxStamina = Stamina;

    }

    void Update()
    {
        //getting inputs for movement and applying them
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        //Player Sprint
        if (Input.GetKey(KeyCode.LeftShift) && Stamina >=0)
        {
            //Stamina functionality
            Stamina -= Time.deltaTime;
            //set isRunning to true
            isRunning = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xInput, yInput) * runSpeed;
        }

        //if not sprinting, or Stamina is depleted
        else
        {
            //set isRunning to false
            isRunning = false;

            //make sure Stamina does not go above initial amount
            //set timers to 0
            if (Stamina >= maxStamina)
            {
                Stamina = maxStamina;
                staminaRegenTimer = 0;
                catchBreathTimer = 0;
            }

            //if Stanima needs to be regenerated
            else
            {
                //checks if the player has used up all of their stamina
                //and if the caughtBreath cooldown has elapsed
                if (catchBreathTimer > catchBreath && Stamina <= 0)
                {
                    staminaRegenTimer += Time.deltaTime;
                    //if time to regen Stamina
                    if (staminaRegenTimer > staminaRegenSpeed)
                    {
                        staminaRegenTimer = 0;
                        Stamina += staminaRegen;
                    }

                    //if NOT time to regen Stamina yet
                    else
                    {
                        staminaRegenTimer += Time.deltaTime;
                    }
                }
                //if Stamina was not depleted, we do not need to catch our breath
                //so regen without catchBreath cooldown
                else if (Stamina > 0)
                    {
                    staminaRegenTimer += Time.deltaTime;
                    //if time to regen, regen
                    if (staminaRegenTimer > staminaRegenSpeed)
                    {
                        staminaRegenTimer = 0;
                        Stamina += staminaRegen;
                    }

                    //if not time to regen, continue timer
                    else
                    {
                        staminaRegenTimer += Time.deltaTime;
                    }

                }

                //if player hasn't rested long enough yet, continue timer
                else
                {
                    catchBreathTimer += Time.deltaTime;
                }

            }
            //walk
            GetComponent<Rigidbody2D>().velocity = new Vector2(xInput, yInput) * walkSpeed;
        }
    }
}
