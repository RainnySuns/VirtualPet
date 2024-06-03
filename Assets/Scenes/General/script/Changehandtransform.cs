using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeArt.Components
{
    class Changehandtransform : MonoBehaviour
    {
        // Start is called before the first frame update
        private WeArtDeviceTrackingObject aa;
        void Start()
        {
            aa = transform.GetComponent<WeArtDeviceTrackingObject>();
            if (transform.name == "WEARTRightHand")
            {
                aa._trackingSource = GameObject.Find("vGear/Frame/User/Hand").transform;
            }
            else if (transform.name == "WEARTLeftHand")
            {
                aa.TrackingSource = GameObject.Find("vGear/Frame/User/Hand2").transform;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
