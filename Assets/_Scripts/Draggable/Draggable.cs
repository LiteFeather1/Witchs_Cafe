using UnityEngine;
using static IDraggable;

public class Draggable : MonoBehaviour, IDraggable
{
    [SerializeField] private DraggingMethod _method = DraggingMethod.Teleport;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;

    public DraggingMethod Method => _method;
    public System.Action OnGrabbed { get; set; }
    public System.Action OnReleased { get; set; }
    public System.Action OnHold { get; set; }
    public System.Action OnForceReleased { get; set; }
    public Rigidbody2D RB => _rb;
    public Collider2D Collider => _collider;
    public bool IsHold { get; set; }
    public bool IsDragging { get; set; }
}
