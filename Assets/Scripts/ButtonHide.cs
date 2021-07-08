using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonHide : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private TMP_Text text;
    
    private bool hide;
    [SerializeField] private float offset = 185f;//half panel for buttons
    private float offsetWidth; 
    private void OnEnable()
    {
        offsetWidth = Screen.width / 2;
        hide = true;
        text.GetComponent<TMP_Text>().text = "<";
    }
    

    public void Hide()
    {
        if (hide)
        {
            text.GetComponent<TMP_Text>().text = ">";
            hide = false;
        }
        else
        {
            text.GetComponent<TMP_Text>().text = "<";
            hide = true;
        }
    }

    private void Update()
    {
        if (mainPanel.transform.localPosition.x != offset * (hide ? 1 : -1))
        {

            mainPanel.transform.localPosition =  new Vector3(Mathf.Lerp(-1*offset, offset, hide ? 1f : 0f) + offsetWidth ,
                mainPanel.transform.localPosition.y,
                mainPanel.transform.localPosition.z);
        }
        
    }

}
