using UnityEngine;

public class ReturnToStartPosAfterRelease : MonoBehaviour
{
    [SerializeField] private float _distanceToTeleport = 1f;
    [SerializeField] private LTFUtils.FixedTimerBehaviour _timerToTeleportBack;
    private IDraggable _draggable;
    private Vector3 _startPos;
    private Quaternion _startRotation;

    private void Awake()
    {
        _draggable = GetComponent<Draggable>();
    }

    private void OnEnable()
    {
        _draggable.Grabbed += OnGrabbed;
        _draggable.Released += OnReleased;
        _timerToTeleportBack.Timer.TimeEvent += Teleport;
    }

    private void OnDisable()
    {
        _draggable.Grabbed -= OnGrabbed;
        _draggable.Released -= OnReleased;
        _timerToTeleportBack.Timer.TimeEvent -= Teleport;
    }

    private void Start()
    {
        transform.GetPositionAndRotation(out _startPos, out _startRotation);
    }

    private void OnGrabbed() => _timerToTeleportBack.Timer.StopAndReset();

    private void OnReleased()
    {
        if (Vector2.Distance(transform.position, _startPos) >= _distanceToTeleport)
            _timerToTeleportBack.Timer.Continue();
    }

    private void Teleport()
    {
        _draggable.RB.velocity = Vector2.zero;
        _draggable.RB.angularVelocity = 0f;
        transform.SetPositionAndRotation(_startPos, _startRotation);
    }
}
