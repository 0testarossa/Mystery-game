using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Border : MonoBehaviour
{
    Image borderImage;
    // Start is called before the first frame update
    void Start()
    {
        borderImage = this.GetComponent<Image>();
        var tempColor = borderImage.color;
        tempColor.a = 0.5f;
        borderImage.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
