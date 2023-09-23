using UnityEngine;

public class DestroyAfterReleaseAndDelay : MonoBehaviour
{
    [SerializeField] private LTFUtils.FixedTimerBehaviour _timerToDelete;
    private IDraggable _draggable;

    private void Awake()
    {
        _draggable = GetComponent<Draggable>();
    }

    private void OnEnable()
    {
        _draggable.Grabbed += OnGrabbed;
        _draggable.Released += OnReleased;
        _draggable.OnHold += OnGrabbed;
        _timerToDelete.Timer.TimeEvent += Destroy;
    }

    private void OnDisable()
    {
        _draggable.Grabbed -= OnGrabbed;
        _draggable.Released -= OnReleased;
        _draggable.OnHold -= OnGrabbed;
        _timerToDelete.Timer.TimeEvent -= Destroy;
    }

    private void OnGrabbed() => _timerToDelete.Timer.StopAndReset();

    private void OnReleased() => _timerToDelete.Timer.Continue();

    private void Destroy() => Destroy(gameObject);
}
