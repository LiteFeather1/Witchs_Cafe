using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanManager : MonoBehaviour
{
    [Header("Area Pannings")]
    [SerializeField] private float _panSpeed = 10f;
    private Vector2 _storePos;
    private Vector3 _currentVelocity;
    private bool _onStore = true;

    [Header("Kitchen")]
    [SerializeField] private Transform _minXPoint;
    [SerializeField] private Transform _maxXPoint;
    [SerializeField, Range(0f, 1f)] private float _percentToPanOnEdges = .975f;
    [SerializeField] private float _speed = 2f;

    [Header("Kitchen")]
    // Screen pos
    [SerializeField] private float _distanceToMinSpeed = .5f;
    [SerializeField] private float _distanceToMaxSpeed = 2f;
    [SerializeField] private float _minSpeed = 1f;
    private float _mouseMiddleClickPos;

    private void Awake() => _storePos = transform.position;

    private void OnEnable()
    {
        InputAction middleClick = GameManager.InputManager.PlayerInputs.MiddleClick;
        middleClick.performed += OnMiddleClick;
        middleClick.canceled += OnMiddleRelease;
    }

    private void OnDisable()
    {
        InputAction middleClick = GameManager.InputManager.PlayerInputs.MiddleClick;
        middleClick.performed -= OnMiddleClick;
        middleClick.canceled -= OnMiddleRelease;
    }

    private void Update()
    {
        if (!_onStore)
        {
            PanOnEdge();
            PanMiddleClick();
        }
    }

    private void OnMiddleClick(InputAction.CallbackContext ctx)
    {
        _mouseMiddleClickPos = Input.mousePosition.x;
    }

    private void OnMiddleRelease(InputAction.CallbackContext ctx)
    {
        _mouseMiddleClickPos = 0f;
    }

    private void MoveCam(Transform cam, Vector3 whereToMove, float speed)
    {
        cam.position = Vector3.MoveTowards(cam.position, whereToMove, speed * Time.deltaTime);
    }

    private void PanOnEdge()
    {
        Vector2 mousePos = Input.mousePosition;
        float rightEdge = Screen.width * _percentToPanOnEdges;
        if (mousePos.x < rightEdge && mousePos.x > Screen.width - rightEdge)
            return;

        float x = GameManager.MousePosition().x;
        if (x <= _minXPoint.position.x)
            x = _minXPoint.position.x;
        else if (x >= _maxXPoint.position.x)
            x = _maxXPoint.position.x;

        var cam = GameManager.Camera.transform;
        Vector3 whereToMove = new(x, cam.position.y, cam.position.z);
        MoveCam(cam, whereToMove, _speed);
    }

    private void PanMiddleClick()
    {
        if (_mouseMiddleClickPos == 0f)
            return;

        var direction = _mouseMiddleClickPos - Input.mousePosition.x;
        float distance = Mathf.Abs(direction);
        if (distance < _distanceToMinSpeed)
            return;

        var cam = GameManager.Camera.transform;

        Vector3 whereToMove = direction < 0f ? _maxXPoint.position : _minXPoint.position;
        whereToMove.z = cam.position.z;
        float speed = Mathf.Lerp(_minSpeed, _speed, distance / _distanceToMaxSpeed);
        MoveCam(cam, whereToMove, speed);
    }

    private IEnumerator PanTo(Vector3 positionToGO)
    {
        var cam = GameManager.Camera.transform;
        Vector3 posToGO = new(positionToGO.x, positionToGO.y, cam.position.z);
        while (Mathf.Abs(cam.position.x - posToGO.x) > 0.01f)
        {
            cam.position = Vector3.SmoothDamp(cam.position, posToGO, ref _currentVelocity, _panSpeed * Time.deltaTime);
            yield return null;
        }
        cam.position = posToGO;
    }

    private IEnumerator PanToKitchenCO()
    {
        yield return PanTo(_minXPoint.position);
        _onStore = false;
    }

    // Called by a button
    public void PanToStore()
    {
        StopAllCoroutines();
        _onStore = true;
        StartCoroutine(PanTo(_storePos));
    }

    // Called by a button
    public void PanToKitchen()
    {
        StopAllCoroutines();
        StartCoroutine(PanToKitchenCO());
    }
}

