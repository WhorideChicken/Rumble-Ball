using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectile : BaseProjectile
{
    public float _rotationSpeed = 270f;

    public override void Init()
    {
        Key = Define.PoolingKey.AxeProjectile;
        ObjType = Define.ObjectType.Projectile;
        Damage = 10f;
        Speed = 30f;
        _mover.SetMovementStrategy(new StraightMovement());
        base.Init();
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.right * _rotationSpeed * Time.fixedDeltaTime);
    }

    protected override float CalculateDamage()
    {
        return Damage;
    }
}
