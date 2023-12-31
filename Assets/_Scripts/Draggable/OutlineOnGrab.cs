﻿using UnityEngine;

public class OutlineOnGrab : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Material _outLineMaterial;
    [SerializeField] private Material _unLineMaterial;
    private IDraggable _draggable;

    private void Awake() => _draggable = GetComponent<IDraggable>();

    private void OnEnable()
    {
        _draggable.OnGrabbed += Outline;
        _draggable.OnReleased += UnOutline;
    }

    private void OnDisable()
    {
        _draggable.OnGrabbed -= Outline;
        _draggable.OnReleased -= UnOutline;
    }

    private void Outline() => _sr.material = _outLineMaterial;

    private void UnOutline() => _sr.material = _unLineMaterial;
}
