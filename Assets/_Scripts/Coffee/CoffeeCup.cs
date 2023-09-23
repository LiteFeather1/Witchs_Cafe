using UnityEngine;

public class CoffeeCup : ReceiveIngredient<ITopping>
{
    [SerializeField] private Coffee _deliverCoffee;
    [SerializeField] private Draggable _coffeeCupDraggable;

    [Header("Points")]
    [SerializeField] private Transform _storePoint;
    [SerializeField] private Transform _kitchenPoint;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer _liquidRenderer;

    [Header("Hover")]
    [SerializeField] private string _title = "Coffee Cup";

    public Coffee DeliverCoffee => _deliverCoffee;

    private void OnMouseEnter()
    {
        if (_t != null)
            GameManager.Instance.HoverInfoManager.SetSimpleText($"Add {_t.Name} to {_title}?");
        else
        {
            var mouseDraggable = GameManager.Instance.MouseManager.Draggable;
            if (mouseDraggable != null)
            {
                if (mouseDraggable.RB.TryGetComponent(out IName nameable))
                    GameManager.Instance.HoverInfoManager.SetSimpleText($"Can't add {nameable.Name} to {_title}!!!");
            }
            else if (_deliverCoffee.CoffeeBean == null
                && _deliverCoffee.Milk == null
                && _deliverCoffee.MiscIngredients.Count == 0)
                GameManager.Instance.HoverInfoManager.SetSimpleText($"{_title} is empty!");
            else
                GameManager.Instance.HoverInfoManager.SetCoffeeText(_title, _deliverCoffee);
        }
    }

    private void OnMouseExit() => GameManager.Instance.HoverInfoManager.DeactiveHover();

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
        _liquidRenderer.color = Color.white;
    }

    // Also Called by a unity event
    public void TeleportToStore()
    {
        transform.position = _storePoint.position;
        _coffeeCupDraggable.enabled = true;
        _coffeeCupDraggable.RB.bodyType = RigidbodyType2D.Dynamic;
        _coffeeCupDraggable.Collider.isTrigger = false;
    }

    // Also Called by a unity event
    public void TeleportToKitchen()
    {
        transform.position = _kitchenPoint.position;
        _coffeeCupDraggable.enabled = false;
        _coffeeCupDraggable.RB.bodyType = RigidbodyType2D.Static;
        _coffeeCupDraggable.Collider.isTrigger = false;
        TrashCoffee();
    }

    protected override void TakeIngredient()
    {
        _t.AddIngredient(_deliverCoffee);
        var t = _t;
        base.TakeIngredient();
        t.Destroy();
    }
}
