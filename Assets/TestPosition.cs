using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TestPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;

        // Print the resolutions
        foreach (var res in resolutions)
        {
            this.GetComponent<Text>().text += res.width + "x" + res.height + " : " + res.refreshRate + "/n";
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
