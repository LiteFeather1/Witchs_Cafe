using UnityEngine;

public class Pestleable : MonoBehaviour, IPestleable
{
    [SerializeField] private TranslatedString _name;
    [SerializeField] private GameObject _objectToGive;
    [SerializeField] private float _resistance = 100f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    private bool _canBeHit;
    public bool CanBeHit => _canBeHit;

    public TranslatedString Name => _name;

    public void SetPosition(Vector2 position)
    {
        _canBeHit = true;
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
        _rb.freezeRotation = true;
        _rb.rotation = 0f;
        _collider.isTrigger = true;
        transform.position = position;
    }

    public void Destroy() => Destroy(gameObject);

    public void Hit(float damage)
    {
        if (!_canBeHit)
            return;

        _resistance -= damage;
        if (_resistance >= 0f)
            return;

        _canBeHit = false;
        Instantiate(_objectToGive, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

