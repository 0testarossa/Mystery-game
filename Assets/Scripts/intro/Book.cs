using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public Image[] allScreens;
    public Image[] borders;
    private int actualBorderIndex;
    private int actualScreenIndex;
    public float timeToShowScreen = 1f;
    private float timeLeftToShowBorder;
    private float timeLeftToShowScreen;
    private bool isChangingOpcacityScreen;
    private bool isChangingOpcacityBorder;
    // Start is called before the first frame update
    void Start()
    {
        actualBorderIndex = 0;
        actualScreenIndex = 0;
        foreach(Image border in borders)
        {
            var tempColor = border.color;
            tempColor.a = 0f;
            border.color = tempColor;
            border.enabled = false;
        }
        foreach(Image screen in allScreens)
        {
            var tempColor = screen.color;
            tempColor.a = 0f;
            screen.color = tempColor;
            screen.enabled = false;
        }
        timeLeftToShowBorder = timeToShowScreen;
        timeLeftToShowScreen = timeToShowScreen;
        isChangingOpcacityScreen = false;
        isChangingOpcacityBorder = false;

        isChangingOpcacityScreen = true;
        allScreens[actualScreenIndex].enabled = true;
        isChangingOpcacityBorder = true;
        borders[actualBorderIndex].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkBorder();
        checkScreen();
    }
    
    private void checkScreen()
    {
        if (isChangingOpcacityScreen)
        {
            if (actualScreenIndex < allScreens.Length)
            {
                timeLeftToShowScreen -= Time.deltaTime;
                var tempColor = allScreens[actualScreenIndex].color;
                tempColor.a = tempColor.a + (1f / timeToShowScreen) * Time.deltaTime;
                allScreens[actualScreenIndex].color = tempColor;
                if (actualScreenIndex > 0)//znikanie zeszlego obrazka
                {
                    var tempColor2 = allScreens[actualScreenIndex-1].color;
                    tempColor2.a = tempColor2.a - (1f / timeToShowScreen) * Time.deltaTime;
                    allScreens[actualScreenIndex-1].color = tempColor2;
                }
                if (timeLeftToShowScreen < 0f)
                {
                    isChangingOpcacityScreen = false;
                    timeLeftToShowScreen = timeToShowScreen;
                    actualScreenIndex++;
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (actualScreenIndex < allScreens.Length && timeLeftToShowScreen == timeToShowScreen)
            {
                isChangingOpcacityScreen = true;
                allScreens[actualScreenIndex].enabled = true;
            }
            else if (actualScreenIndex < allScreens.Length)
            {
                var tempColor = allScreens[actualScreenIndex].color;
                tempColor.a = 1f;
                allScreens[actualScreenIndex].color = tempColor;
                if(actualScreenIndex > 0)//znikanie zeszlego obrazka
                {
                    var tempColor2 = allScreens[actualScreenIndex - 1].color;
                    tempColor2.a = 0f;
                    allScreens[actualScreenIndex - 1].color = tempColor2;
                }



                timeLeftToShowScreen = timeToShowScreen;

                if (actualScreenIndex < allScreens.Length - 1)
                {
                    actualScreenIndex++;
                    isChangingOpcacityScreen = true;
                    allScreens[actualScreenIndex].enabled = true;
                }
                else
                {
                    isChangingOpcacityScreen = false;
                }
            }
        }
    }

    private void checkBorder()
    {
        if (isChangingOpcacityBorder)
        {
            if (actualBorderIndex < 2)
            {
                timeLeftToShowBorder -= Time.deltaTime;
                var tempColor = borders[actualBorderIndex].color;
                tempColor.a = tempColor.a + (1f / timeToShowScreen) * Time.deltaTime;
                borders[actualBorderIndex].color = tempColor;
                if (timeLeftToShowBorder < 0f)
                {
                    isChangingOpcacityBorder = false;
                    timeLeftToShowBorder = timeToShowScreen;
                    actualBorderIndex++;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (actualBorderIndex < 2 && borders[actualBorderIndex].color.a != 1f && timeLeftToShowBorder == timeToShowScreen)
            {
                isChangingOpcacityBorder = true;
                borders[actualBorderIndex].enabled = true;
            }
            else if (actualBorderIndex < 2)
            {
                var tempColor = borders[actualBorderIndex].color;
                tempColor.a = 1f;
                borders[actualBorderIndex].color = tempColor;
                timeLeftToShowBorder = timeToShowScreen;

                if (actualBorderIndex < 1)
                {
                    actualBorderIndex++;
                    isChangingOpcacityBorder = true;
                    borders[actualBorderIndex].enabled = true;
                }
                else
                {
                    isChangingOpcacityBorder = false;
                }
            }
        }
    }
}
