using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDepthCues2D : MonoBehaviour
{
    public Camera mainCameraL;
    public Camera mainCameraR;
    public Transform targetObject;

    public float focusSpeed = 0.1f;
    public float userDepthPerceptionRating;
    public float estimatedDepthAccuracy;


    void Start()
    {
        mainCameraL = GameObject.Find("vGear/Frame/User/Head/MainCamera/MainCamera [L]").GetComponent<Camera>();

        mainCameraR = GameObject.Find("vGear/Frame/User/Head/MainCamera/MainCamera [R]").GetComponent<Camera>();
    }


    void Update()
    {
        AdjustDepthCues();
    }

    void AdjustDepthCues()
    {
        if (userDepthPerceptionRating < 3)
        {
            AdjustStereoDisparity();
        }

        if (estimatedDepthAccuracy < 0.5)
        {
            AdjustMotionParallax();
        }
    }

    void AdjustStereoDisparity()
    {
        mainCameraL.stereoSeparation *= 1.1f;
    }

    void AdjustMotionParallax()
    {
        float distance = Vector3.Distance(mainCameraL.transform.position, targetObject.position);
        targetObject.localPosition += new Vector3(0, 0, distance * 0.1f * Time.deltaTime);
    }
    void AdjustFOV()
    {
        float targetFOV = 60 + (Vector3.Distance(mainCameraL.transform.position, targetObject.position) * 0.5f);
        mainCameraL.fieldOfView = Mathf.Lerp(mainCameraL.fieldOfView, targetFOV, focusSpeed * Time.deltaTime);
    }
}
