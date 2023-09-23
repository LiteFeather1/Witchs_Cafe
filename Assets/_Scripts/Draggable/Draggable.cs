using UnityEngine;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDraggable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Material _outLineMaterial;
    [SerializeField] private Material _unLineMaterial;

    public System.Action Grabbed { get; set; }
    public System.Action Released { get; set; }
    public System.Action OnHold { get; set; }
    public System.Action OnForceReleased { get; set; }
    public Rigidbody2D RB => _rb;
    public Collider2D Collider => _collider;
    public bool IsHold { get; set; }
    public bool IsDragging { get; set; }

    private void OnEnable()
    {
        Grabbed += Outline;
        Released += UnOutline;
    }

    private void OnDisable()
    {
        Grabbed -= Outline;
        Released -= UnOutline;
    }

    private void Outline() => _sr.material = _outLineMaterial;

    private void UnOutline() => _sr.material = _unLineMaterial;
}
