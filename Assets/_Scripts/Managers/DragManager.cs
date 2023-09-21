using UnityEngine;
using UnityEngine.InputSystem;

public class DragManager : MonoBehaviour
{
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private AnimationCurve _speedCurve;
    [SerializeField] private float _distanceToMaxSpeed;
    [SerializeField] private LayerMask _layerMasks;
    private IDraggable _draggable;

    private void OnEnable()
    {
        InputAction leftClick = GameManager.InputManager.PlayerInputs.LeftClick;
        leftClick.performed += OnClick;
        leftClick.canceled += OnReleaseClick;
    }

    private void FixedUpdate()
    {
        if (_draggable == null)
            return;

        Vector2 mousePos = GameManager.MousePosition();
        float distance = Vector2.Distance(_draggable.RB.position, mousePos);
        if (distance < .01f * _speedRange.x)
        {
            _draggable.RB.position = mousePos;
            _draggable.RB.velocity = Vector2.zero;
            return;
        }

        float t = _speedCurve.Evaluate(distance / _distanceToMaxSpeed);
        float speed = Mathf.Lerp(_speedRange.x, _speedRange.y, t);
        Vector2 direction = (mousePos - _draggable.RB.position).normalized;
        _draggable.RB.velocity = speed * direction;
    }

    private void OnDisable()
    {
        InputAction leftClick = GameManager.InputManager.PlayerInputs.LeftClick;
        leftClick.performed -= OnClick;
        leftClick.canceled -= OnReleaseClick;
    }

    private void OnClick(InputAction.CallbackContext ctx)
    {
        Vector2 mousePoint = GameManager.MousePosition();
        var collider = Physics2D.OverlapPoint(mousePoint, _layerMasks);
        if (collider == null)
            return;

        if (collider.TryGetComponent(out IDraggable draggable))
            StartDrag(draggable);
        else if (collider.TryGetComponent(out IGiveDraggable giveDraggable))
            StartDrag(giveDraggable.GetDraggable(mousePoint));
    }

    public void StartDrag(IDraggable draggable)
    {
        if (draggable.Hold) 
            return;

        draggable.OnForceReleased += OnForceRelease;
        draggable.StartDragging();
        _draggable = draggable;
    }

    private void OnReleaseClick(InputAction.CallbackContext ctx)
    {
        if (_draggable == null)
            return;

        OnForceRelease();
    }

    private void OnForceRelease()
    {
        _draggable.OnForceReleased -= OnForceRelease;
        _draggable.StopDragging();
        _draggable = null;
    }
}
