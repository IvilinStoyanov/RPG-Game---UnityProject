using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public static float currentPlayerHealth;
    public static float totalHealthBar;
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();

    }
    //Update is called once per frame
    void Update()
    {
        text.text = "Health : " + currentPlayerHealth + " / " + totalHealthBar;
    }
}
