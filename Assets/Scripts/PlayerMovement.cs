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
    [SerializeField]
    Image staminaWheel;
    float maxStamina;
    [SerializeField]
    float staminaRegen = 0.5f;
    [SerializeField]
    float staminaRegenSpeed = 1f;
    float staminaRegenTimer;
    [SerializeField]
    float catchBreath = 4f;
    [SerializeField]
    float catchBreathSpeed = 5f;
    float catchBreathTimer;
    public bool catchingBreath = false;
    public bool isRunning = false;
    //[SerializeField]
    //float crouchSpeed = 5f;

    void Start()
    {
        maxStamina = Stamina;
        staminaWheel.fillAmount = Stamina / maxStamina;

    }

    void Update()
    {
        //getting inputs for movement and applying them
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (GetComponent<PlayerHealth>().adrenalRushActive == false)
        {
            //Player Sprint
            if (Input.GetKey(KeyCode.LeftShift) && Stamina >= 0)
            {
                //set isRunning to true
                isRunning = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(xInput, yInput) * runSpeed;
                if (GetComponent<Rigidbody2D>().velocity != new Vector2(0, 0))
                {
                    //Stamina functionality
                    Stamina -= Time.deltaTime;
                    staminaWheel.fillAmount = Stamina / maxStamina;
                }

            }

            //if not sprinting, or Stamina is depleted
            else if (catchingBreath == false)
            {
                //set isRunning to false
                isRunning = false;

                //make sure Stamina does not go above initial amount
                //set timers to 0
                if (Stamina >= maxStamina)
                {
                    Stamina = maxStamina;
                    staminaWheel.fillAmount = Stamina / maxStamina;
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
                            staminaWheel.fillAmount = Stamina / maxStamina;
                        }

                        //if NOT time to regen Stamina yet
                        else
                        {
                            staminaRegenTimer += Time.deltaTime;
                        }
                    }
                    else if (catchBreathTimer < catchBreath && Stamina <= 0)
                    {
                        catchingBreath = true;
                    }

                    //if Stamina was not depleted, we do not need to catch our breath
                    //so regen without catchBreath cooldown
                    if (Stamina > 0)
                    {
                        staminaRegenTimer += Time.deltaTime;
                        //if time to regen, regen
                        if (staminaRegenTimer > staminaRegenSpeed)
                        {
                            staminaRegenTimer = 0;
                            Stamina += staminaRegen;
                            staminaWheel.fillAmount = Stamina / maxStamina;
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
            else if (catchingBreath == true)
            {
                catchBreathTimer += Time.deltaTime;
                GetComponent<Rigidbody2D>().velocity = new Vector2(xInput, yInput) * catchBreathSpeed;
                if (catchBreathTimer > catchBreath)
                {
                    catchingBreath = false;
                    catchBreathTimer = 0;
                    staminaRegenTimer = 0;
                    Stamina += staminaRegen;
                    staminaWheel.fillAmount = Stamina / maxStamina;
                }
            }

        }
        else
        {
            //Player Sprint
            if (Input.GetKey(KeyCode.LeftShift) && Stamina >= 0)
            {
                //set isRunning to true
                isRunning = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(xInput, yInput) * runSpeed * (0.5f / GetComponent<PlayerHealth>().adrenalRushTimeSpeed);
                if (GetComponent<Rigidbody2D>().velocity != new Vector2(0, 0))
                {
                    //Stamina functionality
                    Stamina -= Time.deltaTime * (0.5f / GetComponent<PlayerHealth>().adrenalRushTimeSpeed);
                    staminaWheel.fillAmount = Stamina / maxStamina;
                }

            }

            //if not sprinting, or Stamina is depleted
            else if (catchingBreath == false)
            {
                //set isRunning to false
                isRunning = false;

                //make sure Stamina does not go above initial amount
                //set timers to 0
                if (Stamina >= maxStamina)
                {
                    Stamina = maxStamina;
                    staminaWheel.fillAmount = Stamina / maxStamina;
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
                        staminaRegenTimer += Time.deltaTime * (0.5f / GetComponent<PlayerHealth>().adrenalRushTimeSpeed);
                        //if time to regen Stamina
                        if (staminaRegenTimer > staminaRegenSpeed)
                        {
                            staminaRegenTimer = 0;
                            Stamina += staminaRegen;
                            staminaWheel.fillAmount = Stamina / maxStamina;
                        }

                        //if NOT time to regen Stamina yet
                        else
                        {
                            staminaRegenTimer += Time.deltaTime * (0.5f / GetComponent<PlayerHealth>().adrenalRushTimeSpeed);
                        }
                    }
                    else if (catchBreathTimer < catchBreath && Stamina <= 0)
                    {
                        catchingBreath = true;
                    }

                    //if Stamina was not depleted, we do not need to catch our breath
                    //so regen without catchBreath cooldown
                    if (Stamina > 0)
                    {
                        staminaRegenTimer += Time.deltaTime * (0.5f / GetComponent<PlayerHealth>().adrenalRushTimeSpeed);
                        //if time to regen, regen
                        if (staminaRegenTimer > staminaRegenSpeed)
                        {
                            staminaRegenTimer = 0;
                            Stamina += staminaRegen;
                            staminaWheel.fillAmount = Stamina / maxStamina;
                        }

                        //if not time to regen, continue timer
                        else
                        {
                            staminaRegenTimer += Time.deltaTime * (0.5f / GetComponent<PlayerHealth>().adrenalRushTimeSpeed);
                        }

                    }

                    //if player hasn't rested long enough yet, continue timer
                    else
                    {
                        catchBreathTimer += Time.deltaTime * (0.5f / GetComponent<PlayerHealth>().adrenalRushTimeSpeed);
                    }

                }

                //walk
                GetComponent<Rigidbody2D>().velocity = new Vector2(xInput, yInput) * walkSpeed * (0.5f / GetComponent<PlayerHealth>().adrenalRushTimeSpeed);

            }
            else if (catchingBreath == true)
            {
                catchBreathTimer += Time.deltaTime * (0.5f / GetComponent<PlayerHealth>().adrenalRushTimeSpeed);
                GetComponent<Rigidbody2D>().velocity = new Vector2(xInput, yInput) * catchBreathSpeed * (0.5f / GetComponent<PlayerHealth>().adrenalRushTimeSpeed);
                if (catchBreathTimer > catchBreath)
                {
                    catchingBreath = false;
                    catchBreathTimer = 0;
                    staminaRegenTimer = 0;
                    Stamina += staminaRegen;
                    staminaWheel.fillAmount = Stamina / maxStamina;
                }
            }
        }
    }
       
}
