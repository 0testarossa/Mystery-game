using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IndicatorNPC : MonoBehaviour
{
    GameObject message;
    bool isPlayerNear;
    bool isIndicationPanel;
    int selectedColumn;
    int lastSelectedColumn;
    private int[] row = new int[3] {0, 0, 0};
    private int[] lastRow = new int[3] { 1, 1, 1 };
    

    // Start is called before the first frame update
    void Start()
    {
        message = GameObject.Find("Message");
        isPlayerNear = false;
        isIndicationPanel = false;
        selectedColumn = 0;
        lastSelectedColumn = 1;
        GameObject CheckButton = GameObject.Find("IndicateButton");
        Button button = CheckButton.GetComponent<Button>();
        button.onClick.AddListener(onClickButton);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            isIndicationPanel = !isIndicationPanel;
            if (isIndicationPanel)
            {
                GameObject indicationView = GameObject.Find("BackpackView");
                indicationView.GetComponent<Canvas>().enabled = true;

            }
            else
            {
                GameObject indicationView = GameObject.Find("BackpackView");
                indicationView.GetComponent<Canvas>().enabled = false;
            }
        }

        if (isIndicationPanel)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (row[selectedColumn] > 0)
                {
                    row[selectedColumn]--;
                }
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (row[selectedColumn] < 4)
                {
                    row[selectedColumn]++;
                }
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (selectedColumn > 0)
                {
                    selectedColumn--;
                }
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (selectedColumn < 2) selectedColumn++;
            }

            //zmiana podejrzanych
            if(lastRow[0] != row[0])
            {
                GameObject lastSuspected = GameObject.Find("Suspect ("+ lastRow[0] +")");
                lastSuspected.GetComponent<Text>().color = Color.white;

                GameObject suspected = GameObject.Find("Suspect (" + row[0] + ")");
                suspected.GetComponent<Text>().color = Color.green;

                lastRow[0] = row[0];

            }

            //zmiana przedmiotow
            if (lastRow[1] != row[1])
            {
                GameObject lastItem = GameObject.Find("Item (" + lastRow[1] + ")");
                lastItem.GetComponent<Text>().color = Color.white;

                GameObject item = GameObject.Find("Item (" + row[1] + ")");
                item.GetComponent<Text>().color = Color.green;

                lastRow[1] = row[1];

            }

            //zmiana przedmiotow
            if (lastRow[2] != row[2])
            {
                GameObject lastPlace = GameObject.Find("Place (" + lastRow[2] + ")");
                lastPlace.GetComponent<Text>().color = Color.white;

                GameObject place = GameObject.Find("Place (" + row[2] + ")");
                place.GetComponent<Text>().color = Color.green;

                lastRow[2] = row[2];
            }

            if (lastSelectedColumn != selectedColumn)
            {
                GameObject lastColumn = GameObject.Find("Column (" + lastSelectedColumn + ")");
                lastColumn.GetComponent<Text>().color = Color.white;

                GameObject column = GameObject.Find("Column (" + selectedColumn + ")");
                column.GetComponent<Text>().color = Color.red;

                lastSelectedColumn = selectedColumn;
            }

        }
    }

    void onClickButton()
    {
        GameObject suspectObject = GameObject.Find("Suspect (" + row[0] + ")");
        GameObject itemObject = GameObject.Find("Item (" + row[1] + ")");
        GameObject placeObject = GameObject.Find("Place (" + row[2] + ")");

        bool isValid = true;
        if (suspectObject.GetComponent<Text>().text == "EMPTY") isValid = false;
        else if (itemObject.GetComponent<Text>().text == "EMPTY") isValid = false;
        else if (placeObject.GetComponent<Text>().text == "EMPTY") isValid = false;

        if (!isValid)
        {
            message.GetComponent<Text>().text = "You must have missed some facts!";
        }
        else
        {
            if(suspectObject.GetComponent<Text>().text == "cactus" &&
                itemObject.GetComponent<Text>().text == "Fork" &&
                placeObject.GetComponent<Text>().text == "Yellow car")
            {
                //WIN!!!
                SceneManager.LoadScene("Win");
            }
            else
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.name == "Player")
        {
            message.GetComponent<Text>().text = "Press [F] to talk with priest";
            isPlayerNear = true;

        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.name == "Player")
        {
            message.GetComponent<Text>().text = "";
            isPlayerNear = false;

            GameObject indicationView = GameObject.Find("BackpackView");
            indicationView.GetComponent<Canvas>().enabled = false;

        }
    }
}
