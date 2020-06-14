using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public bool isBackpackOpened;
    private GameObject backpackView; 
    // Start is called before the first frame update
    void Start()
    {
        isBackpackOpened = false;
        backpackView = GameObject.Find("BackpackView");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            print(GameObject.Find("IndicateButton"));
            isBackpackOpened = !isBackpackOpened;
            backpackView.GetComponent<Canvas>().enabled = isBackpackOpened;
        }
    }
}
