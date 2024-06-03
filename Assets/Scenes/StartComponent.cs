using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartComponent : MonoBehaviour
{
    public GameObject user;
    // Start is called before the first frame update
    private void Awake()
    {
        user.SetActive(true);
        Debug.Log(user.name);
    }
    void Start()
    {
        
       
        Transform mainCameraTransform = user.transform.Find("main camera");
        if (mainCameraTransform != null)
        {
            mainCameraTransform.gameObject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
