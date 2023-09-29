using UnityEngine;

public class Mortar : ReceiveIngredient<IPestleable>
{
    [SerializeField] private Transform _pointToHold;
    [SerializeField] private Collider2D _collider;

    [Header("Hover")]
    [SerializeField] private TranslatedString _name;
    private IPestleable _pestleable;

    private void OnMouseEnter() => SetHoverText();

    private void OnMouseExit() => GameManager.Instance.HoverInfoManager.HideHover();

    private void SetHoverText()
    {
        if (_t != null)
        {
            switch (GameManager.Instance.Language)
            {
                case Languages.Portuguese:
                    GameManager.Instance.HoverInfoManager.SetSimpleText($"Adicionar {_t.Name.EN_String} no {_name.PT_String}?");
                    break;
                default:
                    GameManager.Instance.HoverInfoManager.SetSimpleText($"Drop {_t.Name.EN_String} to {_name.EN_String}?");
                    break;
            };
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
                            GameManager.Instance.HoverInfoManager.SetSimpleText($"Nao Posso {nameable.Name.PT_String} no {_name.PT_String}!!!");
                            break;
                        default:
                            GameManager.Instance.HoverInfoManager.SetSimpleText($"Can't add {nameable.Name.EN_String} to {_name.EN_String}!!!");
                            break;
                    };
                }
            }
            else if (_pestleable == null)
            {
                switch (GameManager.Instance.Language)
                {
                    case Languages.Portuguese:
                        GameManager.Instance.HoverInfoManager.SetSimpleText($"{_name.PT_String} vazio!");
                        break;
                    default:
                        GameManager.Instance.HoverInfoManager.SetSimpleText($"{_name.EN_String} is empty!");
                        break;
                };
            }
        }
    }

    protected override void TakeIngredient()
    {
        _t.SetPosition(_pointToHold.position);
        _pestleable = _t;
        base.TakeIngredient();

        if (_collider.bounds.Contains(GameManager.MousePosition()))
            SetHoverText();
    }

    protected override void OnFound(IPestleable pestleable)
    {
        _t?.Destroy();
        base.OnFound(pestleable);
    }
}
