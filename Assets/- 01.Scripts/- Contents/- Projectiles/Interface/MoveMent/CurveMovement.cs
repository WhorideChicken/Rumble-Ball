using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : IProjectileMovement
{
    private Vector3 startPoint, controlPoint, endPoint;
    private float t;

    public CurveMovement(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint)
    {
        this.startPoint = startPoint;
        this.controlPoint = controlPoint;
        this.endPoint = endPoint;
        this.t = 0;
    }

    public void Move(Transform projectileTransform, Vector3 direction, float speed)
    {
        t += Time.deltaTime * speed;
        projectileTransform.position = CalculateBezierPosition(startPoint, controlPoint, endPoint, Mathf.Clamp01(t));
    }

    private Vector3 CalculateBezierPosition(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1 - t;
        return (u * u * start) + (2 * u * t * control) + (t * t * end);
    }
}
