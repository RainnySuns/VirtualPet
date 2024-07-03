using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vGear;

public class TestPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vGear.hand != null)
        {
            transform.position = vGear.hand.position;
            transform.rotation = vGear.hand.rotation;
        }
    }
}
