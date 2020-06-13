using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookText : MonoBehaviour
{
    private Text textComponent;
    private Text textComponent2;
    private bool showNextText;
    private int fromDialogPage;
    private int toDialogPage;
    private int currentDialogPage;
    private bool canSkipText;
    private IEnumerator coroutine;
    private string[] plot = {"My new world came here just after i said something i shouldnt have said before. Its good because i didnt want to worry anyone about that", "drugi jestem i Cie po prostu zjem drugi jestem i Cie po prostu zjem drugi jestem i Cie po prostu zjem",
    "trzeci jestem sobie hej hej", "no i czwarty oczywiscie!", "piaty", "szosty"};

    // Start is called before the first frame update
    void Start()
    {
        fromDialogPage = 0;
        toDialogPage = plot.Length - 1;
        canSkipText = false;
        textComponent = GameObject.Find("TextLeft").GetComponent<Text>();
        textComponent.text = "";
        textComponent2 = GameObject.Find("TextRight").GetComponent<Text>();
        textComponent2.text = "";
        showNextText = true;
        currentDialogPage = fromDialogPage;
        coroutine = AnimateText(plot[currentDialogPage]);
    }

    // Update is called once per frame
    void Update()
    {
        if (showNextText)
        {
            showNewPlot();
        }

        if (Input.GetKeyDown("space"))
        {
            if (currentDialogPage < toDialogPage)
            {
                canSkipText = true;
                StopCoroutine(coroutine);
                currentDialogPage++;
                coroutine = AnimateText(plot[currentDialogPage]);
                showNextText = true;
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void showNewPlot()
    {
        StartCoroutine(coroutine);
        showNextText = false;
    }

    public IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        string str = "";
        while (i < strComplete.Length)
        {
            if (canSkipText)
            {
                canSkipText = false;
                //break;
            }
            str += strComplete[i++];
            if(currentDialogPage % 2 == 0)
            {
                textComponent.text = str;
            } else
            {
                textComponent2.text = str;
            }
            yield return new WaitForSeconds(0.04f);
        }
        yield return null;
    }
}
