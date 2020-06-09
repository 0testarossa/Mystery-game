using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class backpack : MonoBehaviour
{

    private int itemsAmount;
    // Start is called before the first frame update
    void Start()
    {
        itemsAmount = 0;
    }

    public void putItem(string name)
    {
        string slotName = "Item (" + itemsAmount.ToString() + ")";
        GameObject itemSlot = GameObject.Find(slotName);
        itemSlot.GetComponent<Text>().text = name;
        
    }

    // Update is called once per frame
    void Update()
    {
       

        
    }
}
