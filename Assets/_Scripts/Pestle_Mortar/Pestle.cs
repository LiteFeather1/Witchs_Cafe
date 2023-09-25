using UnityEngine;

public class Pestle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 _damageRange = new(10f, 25f);
    [SerializeField] private float _velocityForMaxDamage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out IPestleable pestleable))
            return;

        float t = _rb.velocity.magnitude / _velocityForMaxDamage;
        if (t > 1f)
            t = 1f;

        float damage = _damageRange.x + (_damageRange.y - _damageRange.x) * t;
        pestleable.Hit(damage);
    }
}
