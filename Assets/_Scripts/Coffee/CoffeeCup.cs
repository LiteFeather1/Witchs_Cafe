using UnityEngine;

public class CoffeeCup : MonoBehaviour
{
    [SerializeField] private Coffee _deliverCoffee;
    [SerializeField] private Draggable _draggable;

    [Header("Points")]
    [SerializeField] private Transform _storePoint;
    [SerializeField] private Transform _kitchenPoint;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer _liquidRenderer;

    public Coffee DeliverCoffee => _deliverCoffee;

    public bool ReceiveCoffee(Coffee from)
    {
        if (_deliverCoffee.CoffeeBean != null)
            return false;

        _liquidRenderer.color = from.CoffeeBean.Colour;
        _deliverCoffee = from;
        return true;
    }

    // Also Called by a unity event
    public void TrashCoffee()
    {
        _deliverCoffee = new();
        _liquidRenderer.color = Color.clear;
    }

    // Also Called by a unity event
    public void TeleportToStore()
    {
        transform.position = _storePoint.position;
        _draggable.enabled = true;
        _draggable.RB.bodyType = RigidbodyType2D.Dynamic;
        _draggable.Collider.isTrigger = false;
    }

    // Also Called by a unity event
    public void TeleportToKitchen()
    {
        transform.position = _kitchenPoint.position;
        _draggable.enabled = false;
        _draggable.RB.bodyType = RigidbodyType2D.Static;
        _draggable.Collider.isTrigger = false;
        TrashCoffee();
    }
}
