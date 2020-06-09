using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemName;
    private backpack backpackScript;
    private float timer;
    public bool canGet;
    public bool isPlayerNear;

    GameObject message;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        message = GameObject.Find("Message");
        backpackScript = player.GetComponent<backpack>();
        timer = 0;
        canGet = false;
        isPlayerNear = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.5)
        {
            canGet = true;
        }

        if (isPlayerNear && Input.GetKey(KeyCode.F))
        {
            message.GetComponent<Text>().text = "";
            backpackScript.putItem(itemName);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && canGet == true )
        {

            message.GetComponent<Text>().text = "Press [F] to take pick item";
            isPlayerNear = true;

        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.name == "Player" && canGet == true)
        {
            message.GetComponent<Text>().text = "";
            isPlayerNear = false;
        }
    }
}
