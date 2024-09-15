using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    private IProjectileMovement _movementStrategy;
    private Coroutine _movementCoroutine;

    public void SetMovementStrategy(IProjectileMovement movementStrategy)
    {
        _movementStrategy = movementStrategy;
    }

    public void StartMovement(Transform projectileTransform, Vector3 direction, float speed)
    {
        StopMovement();
        _movementCoroutine = StartCoroutine(MoveCoroutine(projectileTransform, direction, speed));
    }

    private IEnumerator MoveCoroutine(Transform projectileTransform, Vector3 direction, float speed)
    {
        while (true)
        {
            _movementStrategy.Move(projectileTransform, direction, speed);
            yield return null;
        }
    }

    public void StopMovement()
    {
        if (_movementCoroutine != null)
        {
            StopCoroutine(_movementCoroutine);
            _movementCoroutine = null;
        }
    }
}
