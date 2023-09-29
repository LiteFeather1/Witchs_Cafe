using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static IDraggable;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMasks;
    [SerializeField] private LayerMask _draggingLayer;

    [Header("Dragging")]
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private AnimationCurve _speedCurve;
    [SerializeField] private float _distanceToMaxSpeed;
    private int _draggablePrevLayer;
    private IDraggable _draggable;
    [SerializeField] private Collider2D[] _colliderResultOfDraggable = new Collider2D[3];
    private readonly IDraggable[] _resultOfDraggable = new IDraggable[3];

    [Header("Audio")]
    [SerializeField] private AudioClip _audioObjectGrabbed;
    [SerializeField] private AudioClip _audioObjectReleased;

    public IDraggable Draggable => _draggable;

    private void OnEnable()
    {
        InputAction leftClick = GameManager.InputManager.PlayerInputs.LeftClick;
        leftClick.performed += OnClick;
        leftClick.canceled += OnReleaseClick;

        GameManager.InputManager.PlayerInputs.RightClick.performed += DeleteIngredient;
    }

    private void FixedUpdate() => DragMovement();

    private void OnDisable()
    {
        InputAction leftClick = GameManager.InputManager.PlayerInputs.LeftClick;
        leftClick.performed -= OnClick;
        leftClick.canceled -= OnReleaseClick;

        GameManager.InputManager.PlayerInputs.RightClick.performed -= DeleteIngredient;
    }

    private void DragMovement()
    {
        if (_draggable == null)
            return;

        Vector2 mousePos = GameManager.MousePosition();
        if (_draggable.Method == DraggingMethod.Move)
        {
            float distance = Vector2.Distance(_draggable.RB.position, mousePos);
            if (distance < _speedRange.x * .01f)
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
        else
            _draggable.RB.position = mousePos;
    }

    private void OnClick(InputAction.CallbackContext ctx)
    {
        Vector2 mousePoint = GameManager.MousePosition();
        Physics2D.OverlapPointNonAlloc(mousePoint, _colliderResultOfDraggable, _layerMasks);

        for (int i = 0; i < _colliderResultOfDraggable.Length; i++)
        {
            if (_colliderResultOfDraggable[i] == null)
                continue;

            if (_colliderResultOfDraggable[i].TryGetComponent(out IGiveDraggable giveDraggable))
            {
                StartDrag(giveDraggable.GetDraggable(mousePoint));
                return;
            }
            _resultOfDraggable[i] = _colliderResultOfDraggable[i].GetComponent<IDraggable>();
        }

        Array.Sort(_resultOfDraggable);
        if (_resultOfDraggable[0] != null)
            StartDrag(_resultOfDraggable[0]);
    }

    public void StartDrag(IDraggable draggable)
    {
        if (draggable.IsHold) 
            return;

        draggable.OnForceReleased += OnForceRelease;
        draggable.StartDragging();
        _draggable = draggable;
        _draggablePrevLayer = draggable.RB.gameObject.layer;
        draggable.RB.gameObject.layer = (int)Mathf.Log(_draggingLayer.value, 2f);
        GameManager.Instance.AudioManager.PlaySFX(_audioObjectGrabbed);
    }

    private void OnReleaseClick(InputAction.CallbackContext ctx)
    {
        if (_draggable == null)
            return;

        OnForceRelease();
        GameManager.Instance.AudioManager.PlaySFX(_audioObjectReleased);
    }

    private void OnForceRelease()
    {
        _draggable.OnForceReleased -= OnForceRelease;
        _draggable.RB.gameObject.layer = _draggablePrevLayer;
        var draggable = _draggable;
        _draggable = null;
        draggable.StopDragging();
    }

    private void DeleteIngredient(InputAction.CallbackContext ctx)
    {
        Vector2 mousePoint = GameManager.MousePosition();
        var collider = Physics2D.OverlapPoint(mousePoint, _layerMasks);
        if (collider == null)
            return;

        if (collider.TryGetComponent(out IDestroyable destroyable))
            destroyable.Destroy();
    }
}
