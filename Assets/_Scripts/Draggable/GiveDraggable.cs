using UnityEngine;

public class GiveDraggable : MonoBehaviour, IGiveDraggable
{
    [SerializeField] private Draggable _draggableToSpawn;

    public IDraggable GetDraggable(Vector2 posToSpawn)
    {
        return Instantiate(_draggableToSpawn, posToSpawn, Quaternion.identity);
    }
}
