using UnityEngine;

public interface IPestleable : IDestroyable
{
    public IName Name { get; }
    public bool CanBeHit { get; }
    public void SetPosition(Vector2 position);
    public void Hit(float damage);
}
