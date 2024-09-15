using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseCointObject : Pool
{
    [SerializeField] private PlayerPosition _targetPosition;

    private bool _isGrounded = true;
    private Tween _movementTween;
    private void OnEnable()
    {
        _movementTween = transform.DOMoveY(2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        _movementTween?.Kill();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(_targetPosition.Value, transform.position) < 10f)
        {
            _movementTween?.Kill();
            transform.position = Vector3.Slerp(transform.position, _targetPosition.Value, Time.deltaTime * 3f);
        }
    }
}
