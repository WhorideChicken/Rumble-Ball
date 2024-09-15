using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CrowProjectile : BaseProjectile
{
    public Vector3 StartPos { get; private set; }
    public Vector3 CenterPos { get; private set; }
    public Transform TargetPos { get; private set; }

    private Coroutine _bezierCoroutine = null;
    private float _timerCurrent = 0.0f;
    private const float DefaultTargetTime = 1.0f;
    private float _targetTime = DefaultTargetTime;

    public override void Init()
    {
        Key = Define.PoolingKey.CrowProjectile;
        ObjType = Define.ObjectType.Projectile;
        Speed = 10f;
        base.Init();
    }


    public void SetBesizePos(Vector3 startPos, Vector3 centerPos, Transform targetPos, float targetTime = DefaultTargetTime)
    {
        StartPos = startPos;
        CenterPos = centerPos;
        TargetPos = targetPos;
        _targetTime = targetTime;
    }


    protected override void StartMovement(Vector3 direction)
    {
        StartBezierMovement();
    }

    private void StartBezierMovement()
    {
        StopBezierCoroutine();
        _timerCurrent = 0.0f;
        _bezierCoroutine = StartCoroutine(BezierMovementCoroutine());
    }

    private IEnumerator BezierMovementCoroutine()
    {
        while (_timerCurrent < _targetTime)
        {
            _timerCurrent += Time.deltaTime;
            float t = Mathf.Clamp01(_timerCurrent / _targetTime);
            transform.position = CalculateBezierPosition(StartPos, CenterPos, TargetPos.position, t);
            yield return null;
        }

        DeSpawn();
    }

    private Vector3 CalculateBezierPosition(Vector3 start, Vector3 center, Vector3 end, float t)
    {
        float u = 1 - t;
        return (u * u * start) + (2 * u * t * center) + (t * t * end);
    }

    public override void DeSpawn()
    {
        StopBezierCoroutine();
        ResetPositions();
        base.DeSpawn();
    }

    private void StopBezierCoroutine()
    {
        if (_bezierCoroutine != null)
        {
            StopCoroutine(_bezierCoroutine);
            _bezierCoroutine = null;
        }
    }

    private void ResetPositions()
    {
        StartPos = Vector3.zero;
        CenterPos = Vector3.zero;
        TargetPos = null;
        _targetTime = DefaultTargetTime;
    }

    protected override float CalculateDamage()
    {
        return Damage; // 필요한 경우 데미지 계산 로직을 여기에 추가
    }
}
