using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFrame : MonoBehaviour
{
    private bool isfull;
    public GameObject frame;
    // Start is called before the first frame update
    void Start()
    {
        isfull = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isfull == true)
        {
            frame.SetActive(true);
        }
    }
}
