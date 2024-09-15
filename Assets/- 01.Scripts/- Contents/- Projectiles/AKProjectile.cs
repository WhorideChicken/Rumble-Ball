using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKProjectile : BaseProjectile
{
    public override void Init()
    {
        Key = Define.PoolingKey.AKProjectile;
        ObjType = Define.ObjectType.Projectile;
        Damage = 10f;
        Speed = 20f;
        _mover.SetMovementStrategy(new StraightMovement());
        base.Init();
    }

    public override void Shoot(Vector3 direction)
    {
        ResetTransform();  
        base.Shoot(direction); 
    }

    public override void ResetTransform()
    {
        transform.SetParent(null);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    protected override void StartMovement(Vector3 direction)
    {
        _mover.StartMovement(transform, direction, Speed);
    }

    protected override float CalculateDamage()
    {
        return Damage;
    }

}
