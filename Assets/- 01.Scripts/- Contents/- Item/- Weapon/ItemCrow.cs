using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class ItemCrow : BaseWeapon
{
    [SerializeField] private PlayerPosition _position;
    private Transform target;

    private void FixedUpdate()
    {
        if (!IsDroppable && Vector3.Distance(_position.Value, this.transform.position) >= 1)
        {
            transform.position = Vector3.Lerp(transform.position, _position.Value, Time.deltaTime * 1);
        }
    }

    public override void Init()
    {
        base.Init();
        _bulletCapacity = 20;
        _reloadTime = 1.0f;
        _intervalTime = 0.2f;
        _shotCount = 5;
    }


    public override void Shoot()
    {
        target = CalculateTarget();
        if (target != null)
        {
            _bullet.GetComponent<CrowProjectile>().SetBesizePos(Vector3.zero, GetCenterPos(), target);
            base.Shoot();
        }
    }


    public Vector3 GetCenterPos()
    {
        return new Vector3(
          transform.position.x + Random.Range(-3f, 3f),
          transform.position.y + Random.Range(-0.5f, 3f),
          transform.position.z + Random.Range(-1f, 1f)
      );
    }


    public Transform GetTarget()
    {
        return target;
    }


    private Transform CalculateTarget()
    {
        Collider[] _target = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(this.transform.position, 30, _target);
        for (int i = 0; i < count; ++i)
        {
            if (_target[i].GetComponent<BaseEnemy>())
            {
                return _target[i].transform;
            }
        }

        return null;
    }

}
