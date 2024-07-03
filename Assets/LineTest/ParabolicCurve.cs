using System;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicCurve
{
    private float a, b, c;

    public ParabolicCurve(float a, float b, float c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    // 生成抛物线上的点
    public List<Vector3> GeneratePoints(int numberOfPoints, Vector3 startPoint, Vector3 endPoint)
    {
        List<Vector3> points = new List<Vector3>();
        float maxHeight = 5.0f; // 最大高度，可以根据需要调整

        for (int i = 0; i <= numberOfPoints; i++)
        {
            float t = (float)i / numberOfPoints;
            float parabola = 4 * maxHeight * t * (1 - t); // 简单的抛物线方程
            Vector3 point = Vector3.Lerp(startPoint, endPoint, t) + new Vector3(0, parabola, 0);
            points.Add(point);
        }

        return points;
    }

    // 根据重量调整抛物线形状
    public void AdjustCurveBasedOnWeight(float weight)
    {
        a += weight * 0.01f;
    }

    public void AdjustCurveBasedOnDistance(float Distance)
    {

    }
}
