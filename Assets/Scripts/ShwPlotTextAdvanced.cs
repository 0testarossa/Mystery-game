using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ShwPlotTextAdvanced : MonoBehaviour
{
    [Serializable]
    public class DialogStepSettings
    {
        public int stepNumber;
    }

    private Text textComponent;
    private bool showNextText;
    public int fromDialogPage;
    public int toDialogPage;
    private int currentDialogPage;
    private bool canSkipText;
    private IEnumerator coroutine;
    //public string scriptToChangeScene;
    private int currentImportantStepIndex;
    [SerializeField]public DialogStepSettings[] imortantSteps;
    /*private string[] nextPlot = { "pierwszy1 " + GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC, "pierwszy2", "pierwszy3", "pierwszy4", "drugi1", "drugi2", "drugi3", "drugi4", "trzci1", "trzeci2",
    "trzeci3", "trzeci4", "czwarty1", "czwarty2", "czwarty3", "czwarty4"};*/
    private string[] nextPlot;
    public string npcColor;
    private bool isDialogAvailable;
    private Dictionary<string, string> plot = new Dictionary<string, string>()
        {
            { "yellow", "yellowText"},
            { "blue", "blueText" },
            { "green", "greenText" },
            { "orange", "orangeText" }
        };

    void Start()
    {
        nextPlot = new string[] { "pierwszy1 " + GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC, "pierwszy2", "pierwszy3", "pierwszy4", "drugi1", "drugi2", "drugi3", "drugi4", "trzci1", "trzeci2",
        "trzeci3", "trzeci4", "czwarty1", "czwarty2", "czwarty3", "czwarty4"};
        isDialogAvailable = false;
        canSkipText = false;
        currentImportantStepIndex = 0;
        textComponent = GameObject.Find("TextNPC").GetComponent<Text>();
        textComponent.text = "";
        showNextText = false;
        currentDialogPage = fromDialogPage;
/*        if (npcColor == GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC)
        {
            coroutine = AnimateText(plot[npcColor]);
            GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().updateCorrectNPC();
            currentDialogPage--;
        }
        else
        {
            coroutine = AnimateText(nextPlot[currentDialogPage]);
        }*/
    }

    void Update()
    {
        nextPlot = new string[] { "pierwszy1 " + GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC, "pierwszy2", "pierwszy3", "pierwszy4", "drugi1", "drugi2", "drugi3", "drugi4", "trzci1", "trzeci2",
        "trzeci3", "trzeci4", "czwarty1", "czwarty2", "czwarty3", "czwarty4"};
        if (showNextText)
        {
            showNewPlot();
        }

        //if (Input.GetKeyDown("space"))
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(isDialogAvailable)
            {
                if (coroutine != null)
                {
                    if (npcColor == GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC ||
                        GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC == "finished")
                    {
                        canSkipText = true;
                        StopCoroutine(coroutine);
                        coroutine = AnimateText(plot[npcColor]);
                        showNextText = true;
                        GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().updateCorrectNPC();
                        plot[npcColor] = "powiedzialem co wiedzialem";
                    }
                    else
                    {
                        if (currentDialogPage < toDialogPage)
                        {
                            canSkipText = true;
                            StopCoroutine(coroutine);
                            currentDialogPage++;

                            coroutine = AnimateText(nextPlot[currentDialogPage]);
                            showNextText = true;
                        }
                        else
                        {
                            currentDialogPage = fromDialogPage;
                            canSkipText = true;
                            StopCoroutine(coroutine);
                            coroutine = AnimateText(nextPlot[currentDialogPage]);
                            showNextText = true;
                        }
                    }
                }
                else
                {
                    if (npcColor == GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC ||
                        GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().correctNPC == "finished")
                    {
                        canSkipText = true;
                        coroutine = AnimateText(plot[npcColor]);
                        showNextText = true;
                        GameObject.Find("NPCPlotLogic").GetComponent<NPCPlotLogic>().updateCorrectNPC();
                        plot[npcColor] = "powiedzialem co wiedzialem";
                    }
                    else
                    {
                        if (currentDialogPage < toDialogPage)
                        {
                            canSkipText = true;
                            currentDialogPage++;

                            coroutine = AnimateText(nextPlot[currentDialogPage]);
                            showNextText = true;
                        }
                        else
                        {
                            currentDialogPage = fromDialogPage;
                            canSkipText = true;
                            coroutine = AnimateText(nextPlot[currentDialogPage]);
                            showNextText = true;
                        }
                    }
                }
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
            }
            str += strComplete[i++];
            textComponent.text = str;
            yield return new WaitForSeconds(0.04f);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Player")
        {
            isDialogAvailable = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            isDialogAvailable = false;
            canSkipText = true;
            textComponent.text = "";
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
    }
}
