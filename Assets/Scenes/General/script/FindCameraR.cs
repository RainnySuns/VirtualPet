using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCameraR : MonoBehaviour
{
    public Canvas canvas;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.GetComponent<Canvas>();
        cam = GameObject.Find("vGear/Frame/User/Head/MainCamera/MainCamera [R]").GetComponent<Camera>();        
        
        if (canvas != null && cam != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = cam;
        }
        else
        {
            Debug.LogError("Canvas or Camera not found");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
