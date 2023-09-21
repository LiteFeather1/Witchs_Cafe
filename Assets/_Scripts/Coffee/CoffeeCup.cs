using UnityEngine;

public class CoffeeCup : MonoBehaviour
{
    [SerializeField] private Coffee _deliverCoffee;

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
}
