using System.Collections;
using UnityEngine;

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

    private void Awake()
    {
        _storePos = transform.position;
    }

    private void Update()
    {
        if (!_onStore)
            PanOnEdge();
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
        cam.position = Vector3.MoveTowards(cam.position, whereToMove, _speed * Time.deltaTime);
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

