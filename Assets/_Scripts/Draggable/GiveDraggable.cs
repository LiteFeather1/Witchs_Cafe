using UnityEngine;

public class GiveDraggable : MonoBehaviour, IGiveDraggable
{
    [SerializeField] private Draggable _draggableToSpawn;

    public System.Action OnGrabbed { get; set; }

    public IDraggable GetDraggable(Vector2 posToSpawn)
    {
        OnGrabbed?.Invoke();
        return Instantiate(_draggableToSpawn, posToSpawn, Quaternion.identity);
    }
}
