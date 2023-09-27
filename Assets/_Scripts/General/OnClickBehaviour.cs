using UnityEngine;
using UnityEngine.Events;

public class OnClickBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent _onClick;

    private void OnMouseDown() => _onClick?.Invoke();
}
