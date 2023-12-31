using UnityEngine;

public class SinAnimator : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private bool _unscaledTime;

    [Header("Movement")]
    [SerializeField] private Vector2 _amplitude;
    [SerializeField] private float _speed;
    private Vector2 _startPos;

    [Header("Rotation")]
    [SerializeField] private float _angle = 11.25f;
    [SerializeField] private float _timeRotation = 5f;
    [SerializeField] private AnimationCurve _rotationCurve;
    private float _elapsedRotationTime;
    private float _direction = 1f;

    public float TimeTime => _unscaledTime ? Time.unscaledTime : Time.time;
    public float DeltaTime => _unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

    private void Awake()
    {
        _startPos = transform.localPosition;
        _elapsedRotationTime = _timeRotation * .5f;
    }

    private void Update()
    {
        // Sin Movement
        var sin = Mathf.Sin(TimeTime * _speed) * _amplitude;
        transform.localPosition = _startPos + sin;

        // Rotation
        _elapsedRotationTime += DeltaTime * _direction;
        float t = _rotationCurve.Evaluate(_elapsedRotationTime / _timeRotation);
        float z = Mathf.LerpAngle(-_angle, _angle, t);
        transform.rotation = Quaternion.Euler(new(0f, 0f, z));
        if (_elapsedRotationTime > _timeRotation || _elapsedRotationTime < 0f)
            _direction *= -1f;
    }
}
