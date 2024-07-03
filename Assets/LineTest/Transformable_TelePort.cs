using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vGear;


public class Transformable_TelePort : MonoBehaviour
{
    private ParabolicCurve curve;
    private LineRenderer lineRenderer;
    private float weight = 0f;
    public vGear_Controller vgear;
    public GameObject point;
    private Vector3 desinationPosition;

    private Vector3 initialPosition;
    private GameObject targetObject = null;
    private float someForceFactor;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Standard"));

        // 设置线的颜色为白色
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        desinationPosition = point.transform.position;

        initialPosition = transform.position;
        curve = new ParabolicCurve(1, 0, 0);

        DrawParabolicCurve(transform.position, desinationPosition);
    }

    void Update()
    {
        if (vGear.Cmd.Received("CallUI"))
        {
            curve.AdjustCurveBasedOnWeight(weight);
            DrawParabolicCurve(transform.position, desinationPosition);
        }

        //应该要放在point里面做collision检测？

        if (targetObject != null)
        {
            float distanceMoved = Vector3.Distance(initialPosition, transform.position);

            curve.AdjustCurveBasedOnDistance(distanceMoved);

            Vector3 forceDirection = (transform.position - initialPosition).normalized;
            float forceMagnitude = distanceMoved * someForceFactor;
            Vector3 force = forceDirection * forceMagnitude;

            Rigidbody rb = targetObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(force);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Gravit weightedObject = collision.gameObject.GetComponent<Gravit>();

        if (weightedObject != null)
        {
            targetObject = collision.gameObject;
            desinationPosition = targetObject.transform.position;
            float objectWeight = weightedObject.Graviter;
            curve.AdjustCurveBasedOnWeight(objectWeight);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        weight = 0f;
        desinationPosition = point.transform.position;
        DrawParabolicCurve(initialPosition, desinationPosition);
    }

    void DrawParabolicCurve(Vector3 startPoint, Vector3 endPoint)
    {
        var points = curve.GeneratePoints(50, startPoint, endPoint);
        lineRenderer.positionCount = points.Count;
        Vector3[] positions = new Vector3[points.Count];

        for (int i = 0; i < points.Count; i++)
        {
            positions[i] = points[i];
        }

        lineRenderer.SetPositions(positions);
    }

}
