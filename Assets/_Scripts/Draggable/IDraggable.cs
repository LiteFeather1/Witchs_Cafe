using UnityEngine;

public interface IDraggable
{
    public System.Action Grabbed { get; set; }
    public System.Action Released { get; set; }
    public System.Action OnHold { get; set; }
    public System.Action OnForceReleased { get; set; }
    public Rigidbody2D RB { get; }
    public Collider2D Collider { get; }
    public bool IsHold { get; set; }
    public bool IsDragging { get; set; }

    public void StartDragging()
    {
        IsDragging = true;
        RB.velocity = Vector2.zero;
        RB.gravityScale = 0f;
        RB.rotation = 0f;
        RB.freezeRotation = true;
        Collider.isTrigger = true;
        Grabbed?.Invoke();
    }

    public void StopDragging()
    {
        IsDragging = false;
        RB.gravityScale = 1f;
        RB.freezeRotation = false;
        Collider.isTrigger = false;
        Released?.Invoke();
    }

    public void Hold()
    {
        IsHold = true;
        OnHold?.Invoke();
    }

    public void ForceRelease() => OnForceReleased?.Invoke();
}
