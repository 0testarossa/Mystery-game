using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    private float time;
    private float numberChangeTime;
    private TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        time = 600.0f;
        numberChangeTime = 75.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(time>0)
        {
            time -= Time.deltaTime;
            if (numberChangeTime > 0)
            {
                numberChangeTime -= Time.deltaTime;
            }
            else
            {
                timerText.text = (Int16.Parse(timerText.text.Remove(timerText.text.Length - 1)) - 1).ToString() + "h";
                numberChangeTime = 75.0f;
            }
        } else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
