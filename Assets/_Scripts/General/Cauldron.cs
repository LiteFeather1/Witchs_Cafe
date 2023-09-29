using UnityEngine;

public class Cauldron : ReceiveIngredient<IMixable>
{
    [Header("Hover")]
    [SerializeField] private TranslatedString _name;

    [Header("Coffee")]
    [SerializeField] private Coffee _mixingCoffee;

    [Header("Components")]
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer[] _waterSprites;
    [SerializeField] private ParticleSystem _particleSystem;

    [Header("Sounds")]
    [SerializeField] private AudioClip _audioIngredientDropped;

    public System.Action<Color> OnCoffeChange { get; set; }

    public Coffee MixingCoffee => _mixingCoffee;

    private void OnEnable() => SubToCoffee();

    private void OnDisable() => UnSubToCoffee();

    private void OnMouseEnter() => SetHoverText();

    private void OnMouseExit() => GameManager.Instance.HoverInfoManager.HideHover();

    public void SubToCoffee() => _mixingCoffee.OnCoffeeBeanSet += OnCoffeeBeanSet;

    public void UnSubToCoffee() => _mixingCoffee.OnCoffeeBeanSet -= OnCoffeeBeanSet;

    private void OnCoffeeBeanSet(IngredientSO ingredient) => SetSpriteColour(ingredient.Colour);

    private void SetHoverText()
    {
        if (_t != null)
        {
            switch (GameManager.Instance.Language)
            {
                case Languages.Portuguese:
                    GameManager.Instance.HoverInfoManager.SetSimpleText($"Adicionar {_t.Name.PT_String} no {_name.PT_String}?");
                    break;
                default:
                    GameManager.Instance.HoverInfoManager.SetSimpleText($"Add {_t.Name.EN_String} to {_name.EN_String}?");
                    break;
            }
        }
        else
        {
            var mouseDraggable = GameManager.Instance.MouseManager.Draggable;
            if (mouseDraggable != null)
            {
                if (mouseDraggable.RB.TryGetComponent(out IName nameable))
                {
                    switch (GameManager.Instance.Language)
                    {
                        case Languages.Portuguese:
                            GameManager.Instance.HoverInfoManager.SetSimpleText($"Não posso {nameable.Name.PT_String} no {_name.PT_String}!!!");
                            break;
                        default:
                            GameManager.Instance.HoverInfoManager.SetSimpleText($"Can't add {nameable.Name.EN_String} to {_name.EN_String}!!!");
                            break;
                    }
                }
            }
            else if (_mixingCoffee.CoffeeBean == null
                && _mixingCoffee.Milk == null
                && _mixingCoffee.MiscIngredients.Count == 0)
            {
                switch (GameManager.Instance.Language)
                {
                    case Languages.Portuguese:
                        GameManager.Instance.HoverInfoManager.SetSimpleText($"{_name.PT_String} vazio!");
                        break;
                    default:
                        GameManager.Instance.HoverInfoManager.SetSimpleText($"{_name.EN_String} is empty!");
                        break;
                }
            }
            else
                GameManager.Instance.HoverInfoManager.SetCoffeeText(_name.String(GameManager.Instance.Language), _mixingCoffee);
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
        GameManager.Instance.AudioManager.PlaySFX(_audioIngredientDropped);

        if (_collider.bounds.Contains(GameManager.MousePosition()))
            SetHoverText();
    }

    private void SetSpriteColour(Color colour)
    {
        for (int i = 0; i < _waterSprites.Length; i++)
            _waterSprites[i].color = colour;

        var particleMain = _particleSystem.main;
        particleMain.startColor = colour;

        OnCoffeChange?.Invoke(colour);
    }
}
