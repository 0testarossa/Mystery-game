using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    private backpack backpackScript;
    private float timer;
    public bool canGet;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        backpackScript = player.GetComponent<backpack>();
        timer = 0;
        canGet = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.5)
        {
            canGet = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && canGet == true )
        {   

            backpackScript.putItem(itemName);
            Destroy( gameObject );


        }
    }
}
