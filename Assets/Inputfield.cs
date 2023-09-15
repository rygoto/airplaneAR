using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inputfield : MonoBehaviour
{
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    // Start is called before the first frame update
    public float GetInputField1Value()
    {
        float value;
        if (float.TryParse(inputField1.text, out value))
        {
            return value;
        }
        else
        {
            return 0.0f;
        }
    }

    public float GetInputField2Value()
    {
        float value;
        if (float.TryParse(inputField2.text, out value))
        {
            return value;
        }
        else
        {
            return 0.0f;
        }
    }

}
