using UnityEngine;

public class Draggable : MonoBehaviour, IDraggable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;

    public System.Action Grabbed { get; set; }
    public System.Action Released { get; set; }
    public System.Action OnHold { get; set; }
    public System.Action OnForceReleased { get; set; }
    public Rigidbody2D RB => _rb;
    public Collider2D Collider => _collider;
    public bool IsHold { get; set; }
    public bool IsDragging { get; set; }
}
