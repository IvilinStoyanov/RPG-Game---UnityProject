using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarScript : MonoBehaviour
{
    public static float currentExp;
    public static float expNeeded;
    public static int level;

    Text text;
    Slider expBar;
    playerLevelSystem playerLevel;

    // Use this for initialization
    void Start()
    {
        playerLevel = GetComponent<playerLevelSystem>();
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = "LEVEL " + level + ": " + currentExp + " / " + expNeeded;
        
    }


}
