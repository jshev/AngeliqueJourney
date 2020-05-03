using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //so we can edit the items in the inspector
public class dialogue {
    public string narrator;
    [TextArea(3, 10)] //the minimim and maximum lines that the text area can use
    public string[] sentences;

    
}
