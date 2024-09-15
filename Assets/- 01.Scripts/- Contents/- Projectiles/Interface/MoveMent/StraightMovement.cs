using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMovement : IProjectileMovement
{
    public void Move(Transform projectileTransform, Vector3 direction, float speed)
    {
        projectileTransform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}
