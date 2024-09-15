using UnityEngine;

public interface IProjectileMovement
{
    void Move(Transform projectileTransform, Vector3 direction, float speed);
}
