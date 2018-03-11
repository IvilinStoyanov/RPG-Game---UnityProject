using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerLevelSystem : MonoBehaviour
{
    public int level = 1;
    public float currentExperiance = 0f;

    public float expNeededForNextLevel = 50f;

    private float experianceGrow = 100f;

    public bool isLevelUp = false;

    PlayerHealth playerHealth;

     public Slider healthSlider;

    public Slider expSlider;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();

        expSlider.value = currentExperiance;
        expSlider.maxValue = expNeededForNextLevel;
        ExpBarScript.currentExp = currentExperiance;
        ExpBarScript.expNeeded = expNeededForNextLevel;

 
    }

    public void GetExperience(float exp)
    {
        currentExperiance += exp;
        ExpBarScript.currentExp += exp;
        expSlider.value = currentExperiance;

        if (currentExperiance >= expNeededForNextLevel)
        {
            //LEVEL UP
            level++;
            expNeededForNextLevel += experianceGrow;
            currentExperiance = 0f;
            isLevelUp = true;

            expSlider.value = currentExperiance;
            expSlider.maxValue = expNeededForNextLevel;
            ExpBarScript.currentExp = currentExperiance;
            ExpBarScript.expNeeded = expNeededForNextLevel;
            //LEVEL UP

            // incr total health with 50 after level up
            playerHealth.totalHealth += 50;
            BarScript.totalHealthBar = playerHealth.totalHealth;
            healthSlider.maxValue = playerHealth.totalHealth;

            playerHealth.TakeCurrentHealthAfterLevelUp(playerHealth.totalHealth);

            //playerHealth.currentHealth = playerHealth.totalHealth;
            // BarScript.currentPlayerHealth = playerHealth.totalHealth;

            Debug.Log("Level Up" + level);
        }
        else
        {
            isLevelUp = false;
        }
    }
}
