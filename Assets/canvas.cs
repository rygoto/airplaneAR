using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvas : MonoBehaviour
{
    public Text displayText;

    // Start is called before the first frame update
    private void Start()
    {
        displayText.text = " ";
    }

    // Update is called once per frame
    public void OnObjectClick(string objectInfo)
    {
        displayText.text = "Clicked Object Info: " + objectInfo;
    }
}
