using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vNet.Database;
using Votanic.vXR.vGear;

public class UICall : MonoBehaviour
{
    public GameObject UI;
    private GameObject currentUIInstance;
    
    void Update()
    {
        if (vGear.Cmd.Received("CallUI"))
        {
            if (currentUIInstance == null)
            {
                currentUIInstance = Instantiate(UI, transform);
                Debug.Log(transform.position);
            }

            else
            {
                Destroy(currentUIInstance.GetComponentInChildren<PrefabCreate>().currentInstance);
                Destroy(currentUIInstance);
                currentUIInstance = null;
            }
        }
    }
}
