using UnityEngine;

public class Mortar : ReceiveIngredient<IPestleable>
{
    [SerializeField] private Transform _pointToHold;
    [SerializeField] private Collider2D _collider;

    [Header("Hover")]
    [SerializeField] private string _title = "Mortar";
    private IPestleable _pestleable;

    private void OnMouseEnter() => SetHoverText();

    private void OnMouseExit() => GameManager.Instance.HoverInfoManager.HideHover();

    private void SetHoverText()
    {
        if (_t != null)
            GameManager.Instance.HoverInfoManager.SetSimpleText($"Drop {_t.Name} to {_title}?");
        else
        {
            var mouseDraggable = GameManager.Instance.MouseManager.Draggable;
            if (mouseDraggable != null)
            {
                if (mouseDraggable.RB.TryGetComponent(out IName nameable))
                    GameManager.Instance.HoverInfoManager.SetSimpleText($"Can't add {nameable.Name} to {_title}!!!");
            }
            else if (_pestleable == null)
                GameManager.Instance.HoverInfoManager.SetSimpleText($"{_title} is empty!");
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
