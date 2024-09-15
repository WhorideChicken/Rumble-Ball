using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpetProjectile : BaseProjectile
{
    public override void Init()
    {
        Key = Define.PoolingKey.TrumpetProjectile;
        ObjType = Define.ObjectType.Projectile;
        Damage = 10f;
        Speed = 20f;
        _mover.SetMovementStrategy(new StraightMovement());
        base.Init();
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
