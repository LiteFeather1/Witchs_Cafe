using UnityEngine;

public class StartClickAnimator : MonoBehaviour
{
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

    private void Awake()
    {
        _startPos = transform.position;
        _elapsedRotationTime = _timeRotation * .5f;
    }

    private void Update()
    {
        // Sin Movment
        var sin = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = _startPos + sin;

        // Rotation
        _elapsedRotationTime += Time.deltaTime * _direction;
        float t = _rotationCurve.Evaluate(_elapsedRotationTime / _timeRotation);
        float z = Mathf.Lerp(-_angle, _angle, t);
        transform.rotation = Quaternion.Euler(new(0f, 0f, z));
        if (_elapsedRotationTime > _timeRotation || _elapsedRotationTime < 0f)
            _direction *= -1f;
    }
}
