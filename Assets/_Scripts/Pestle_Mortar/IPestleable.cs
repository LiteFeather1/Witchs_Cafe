using UnityEngine;

public interface IPestleable : IDestroyable, IName
{
    public bool CanBeHit { get; }
    public void SetPosition(Vector2 position);
    public void Hit(float damage);
}
