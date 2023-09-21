using UnityEngine;

public interface IDraggable
{
    public System.Action Grabbed { get; set; }
    public System.Action Released { get; set; }
    public System.Action OnForceReleased { get; set; }
    public Rigidbody2D RB { get; }
    public Collider2D Collider { get; }
    public bool Hold { get; set; }
    public bool IsDragging { get; set; }

    public void StartDragging()
    {
        if (Hold)
            return;

        IsDragging = true;
        RB.velocity = Vector2.zero;
        RB.gravityScale = 0f;
        RB.rotation = 0f;
        RB.freezeRotation = true;
        Collider.enabled = false;
        Grabbed?.Invoke();
    }

    public void StopDragging()
    {
        IsDragging = false;
        RB.gravityScale = 1f;
        RB.freezeRotation = false;
        Collider.enabled = true;
        Released?.Invoke();
    }

    public void ForceRelease() => OnForceReleased?.Invoke();
}
