using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramove : MonoBehaviour
{
    public float movespeed = 1f;
    bool forwardmove;
    bool backmove;
    bool rightmove;
    bool leftmove;
    bool rise;
    bool descend;

    bool reset;
    // Start is called before the first frame update
    void Start()
    {
        Transform childTransform = transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        Transform childTransform = transform.GetChild(0);
        if (childTransform != null)
        {

            if (forwardmove == true)
            {
                Vector3 horizontalDirection = childTransform.TransformDirection(Vector3.forward);
                horizontalDirection.y = 0;
                transform.position += horizontalDirection.normalized * movespeed * Time.deltaTime;//new Vector3(0, 0, movespeed * Time.deltaTime);
            }
            if (backmove == true)
            {
                Vector3 horizontalDirection = childTransform.TransformDirection(Vector3.forward);
                horizontalDirection.y = 0;
                transform.position += horizontalDirection.normalized * -movespeed * Time.deltaTime;//new Vector3(0, 0, movespeed * Time.deltaTime);
            }
            if (rightmove == true)
            {
                transform.position += childTransform.TransformDirection(Vector3.right) * movespeed * Time.deltaTime;//new Vector3(movespeed * Time.deltaTime, 0, 0);
            }
            if (leftmove == true)
            {
                transform.position += childTransform.TransformDirection(Vector3.left) * movespeed * Time.deltaTime;
                //new Vector3(-movespeed * Time.deltaTime, 0, 0);
            }
            if (rise == true)
            {
                transform.position += new Vector3(0, movespeed * Time.deltaTime, 0);
            }
            if (descend == true)
            {
                transform.position += new Vector3(0, -movespeed * Time.deltaTime, 0);
            }
            if (reset == true)
            {
                transform.position = new Vector3(0, 0, 0);
            }
        }
    }

    public void forwardButtonDown()
    {
        forwardmove = true;
    }
    public void forwardButtonUp()
    {
        forwardmove = false;
    }

    public void backButtonDown()
    {
        backmove = true;
    }
    public void backButtonUp()
    {
        backmove = false;
    }

    public void rightButtonDown()
    {
        rightmove = true;
    }
    public void rightButtonUp()
    {
        rightmove = false;
    }

    public void leftButtonDown()
    {
        leftmove = true;
    }

    public void leftButtonUp()
    {
        leftmove = false;
    }

    public void riseButtonDown()
    {
        rise = true;
    }
    public void riseButtonUp()
    {
        rise = false;
    }
    public void descendButtonDown()
    {
        descend = true;
    }

    public void descendButtonUp()
    {
        descend = false;
    }

    public void resetButtonDown()
    {
        reset = true;
    }

    public void resetButtonUp()
    {
        reset = false;
    }
}
