using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Votanic.vXR;
using Votanic.vXR.vCast;
using Votanic.vXR.vGear;
using Votanic.vXR.vGear.Networking;



public class pick : MonoBehaviour
{
    public GameObject abc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vGear.Cmd.Received("Pick11"))
        {
            Debug.Log("Pick");
            transform.position = abc.transform.position;
        }
    }
}
