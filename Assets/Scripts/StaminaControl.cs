using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaControl : MonoBehaviour
{
    public Image staminaUI;
    public float staminaDuration = 5f;
    public float currentStamina;
    public GameObject player;
    public bool canRun = true;


    // Start is called before the first frame update
    void Start()
    {
        currentStamina = staminaDuration;
        staminaUI.fillAmount = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl playerControl = player.GetComponent<PlayerControl>();
        
        if(playerControl.running && canRun && (playerControl.horizontal != 0 || playerControl.vertical != 0))
        {
            currentStamina -= Time.deltaTime*2;
            staminaUI.fillAmount = currentStamina / staminaDuration;
            playerControl.currentSpeed = playerControl.sprintSpeed;
            
        }
        else 
        {
            currentStamina += Time.deltaTime / 5;
            staminaUI.fillAmount = currentStamina / staminaDuration;
            playerControl.currentSpeed = playerControl.normalSpeed;
        }

        if(currentStamina <= 0.1f)
        {
            canRun = false;
            playerControl.running = false;
            playerControl.currentSpeed = playerControl.normalSpeed;

        }
        else if(currentStamina > 0.1f)
        {
            canRun = true;
        }
        

    }
}
