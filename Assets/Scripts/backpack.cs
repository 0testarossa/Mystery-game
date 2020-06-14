using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class backpack : MonoBehaviour
{

    private int itemsAmount;
    private int placesAmount;
    private int suspectsAmount;
    // Start is called before the first frame update
    void Start()
    {
        itemsAmount = 0;
        placesAmount = 0;
        suspectsAmount = 0;
    }

    public void putItem(string name)
    {
        string slotName = "Item (" + itemsAmount.ToString() + ")";
        GameObject itemSlot = GameObject.Find(slotName);
        itemSlot.GetComponent<Text>().text = name;
        itemsAmount++;
        GameObject backPack = GameObject.Find("BackpackView");
        backPack.SetActive(false);
        backPack.SetActive(true);
    }

    public void putPlace(string name)
    {
        string slotName = "Place (" + placesAmount.ToString() + ")";
        GameObject itemSlot = GameObject.Find(slotName);
        itemSlot.GetComponent<Text>().text = name;
        placesAmount++;
        GameObject backPack = GameObject.Find("BackpackView");
        backPack.SetActive(false);
        backPack.SetActive(true);
    }

    public void putSuspect(string name)
    {
        string slotName = "Suspect (" + suspectsAmount.ToString() + ")";
        GameObject suspectSlot = GameObject.Find(slotName);
        suspectSlot.GetComponent<Text>().text = name;
        suspectsAmount++;
        GameObject backPack = GameObject.Find("BackpackView");
        backPack.SetActive(false);
        backPack.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       

        
    }
}
