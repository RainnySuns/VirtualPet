using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vGear;

public class SetParent : MonoBehaviour
{
    public GameObject finger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vGear.controller.currentTool.name == "Brush")
        {
            setpos();
            //setparent();
            vGear.Cmd.Send("Brush");
        }        
    }

    private void setpos()
    {
        transform.position = finger.transform.position;
    }

    private void setparent()
    {
        transform.SetParent(finger.transform);
    }

}
