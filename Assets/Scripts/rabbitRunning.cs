using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rabbitRunning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1 * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime);
        transform.Rotate(0, -150 * Time.deltaTime, 0);
    }
}
