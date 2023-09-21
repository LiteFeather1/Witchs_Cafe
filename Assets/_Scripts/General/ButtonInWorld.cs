using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonInWorld : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnityAction _onClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _onClick?.Invoke();
    }
}

