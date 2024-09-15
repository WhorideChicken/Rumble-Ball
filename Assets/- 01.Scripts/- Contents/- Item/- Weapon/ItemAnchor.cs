using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnchor : BaseWeapon
{
    private bool _isInitialized = false;
    private BaseProjectile _anchorProjectile = null;

    public override void Init()
    {
        Key = Define.PoolingKey.ItemAnchor;
        _bulletCapacity = 11;
        _reloadTime = 3.0f;
        _intervalTime = 0.2f;
        _shotCount = 1;
        base.Init();
    }

    public override void Shoot()
    {
        if (!_isInitialized)
        {
            InitializeAnchorProjectile();
        }
        else
        {
            // _anchorProjectile.Trajectory();
        }
    }

    private void InitializeAnchorProjectile()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        Pool pool = ObjectPooling.Instance.SpawnWithParent(_bullet.Key, _startPos);
        _anchorProjectile = pool.GetComponent<BaseProjectile>();

        if (_anchorProjectile != null)
        {
            var springJoint = _anchorProjectile.GetComponent<SpringJoint>();
            if (springJoint != null)
            {
                springJoint.connectedBody = GetComponent<Rigidbody>();
            }

            _anchorProjectile.transform.SetParent(null);
        }

        _isInitialized = true;
    }
}
