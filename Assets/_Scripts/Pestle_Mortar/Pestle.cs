using UnityEngine;

public class Pestle : MonoBehaviour
{
    [SerializeField] private Vector2 _damageRange = new(10f, 25f);
    [SerializeField] private float _velocityForMaxDamage = 10f;
    [SerializeField] private Rigidbody2D _rb;
    private IDraggable _draggable;

    [Header("Audio")]
    [SerializeField] private PlaySoundOnHit _playsSoundOnHit;

    public void Awake() => _draggable = GetComponent<IDraggable>();

    private void OnEnable()
    {
        _draggable.OnGrabbed += CanPlaySound;
        _draggable.OnReleased += CantPlaySound;
    }

    private void OnDisable()
    {
        _draggable.OnGrabbed -= CanPlaySound;
        _draggable.OnReleased -= CantPlaySound;
    }

    private void CanPlaySound() => _playsSoundOnHit.SetCanPlay(true);
    private void CantPlaySound() => _playsSoundOnHit.SetCanPlay(false);

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
