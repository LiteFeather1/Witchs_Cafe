using UnityEngine;

public class Cauldron : ReceiveIngredient<IMixable>
{
    [Header("Hover")]
    [SerializeField] private string _title = "Cauldron";

    [Header("Coffee")]
    [SerializeField] private Coffee _mixingCoffee;

    [Header("Components")]
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer[] _waterSprites;

    public System.Action<Color> OnCoffeChange { get; set; }

    public Coffee MixingCoffee => _mixingCoffee;

    private void OnEnable() => SubToCoffee();

    private void OnDisable() => UnSubToCoffee();

    private void OnMouseEnter() => SetHoverText();

    private void OnMouseExit() => GameManager.Instance.HoverInfoManager.DeactiveHover();

    public void SubToCoffee() => _mixingCoffee.OnCoffeeBeanSet += OnCoffeeBeanSet;

    public void UnSubToCoffee() => _mixingCoffee.OnCoffeeBeanSet -= OnCoffeeBeanSet;

    private void OnCoffeeBeanSet(IngredientSO ingredient) => SetSpriteColour(ingredient.Colour);

    private void SetHoverText()
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
            else if (_mixingCoffee.CoffeeBean == null
                && _mixingCoffee.Milk == null
                && _mixingCoffee.MiscIngredients.Count == 0)
                GameManager.Instance.HoverInfoManager.SetSimpleText($"{_title} is empty!");
            else
                GameManager.Instance.HoverInfoManager.SetCoffeeText(_title, _mixingCoffee);
        }
    }

    // Also Called by a unity event
    public void TrashCoffee()
    {
        UnSubToCoffee();
        _mixingCoffee = new();
        SetSpriteColour(Color.white);
        SubToCoffee();
    }

    protected override void TakeIngredient()
    {
        _t.AddIngredient(_mixingCoffee);
        var t = _t;
        base.TakeIngredient();
        t.Destroy();

        if (_collider.bounds.Contains(GameManager.MousePosition()))
            SetHoverText();
    }

    private void SetSpriteColour(Color colour)
    {
        for (int i = 0; i < _waterSprites.Length; i++)
            _waterSprites[i].color = colour;

        OnCoffeChange?.Invoke(colour);
    }
}
