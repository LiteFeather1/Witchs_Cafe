using UnityEngine;

public interface IGiveDraggable
{
    public System.Action OnGrabbed { get; set; }
    public IDraggable GetDraggable(Vector2 posToSpawn);
}
