using UnityEngine;

public class Mortar : ReceiveIngredient<IPestleable>
{
    [SerializeField] private Transform _pointToHold;

    protected override void TakeIngredient()
    {
        _t.SetPosition(_pointToHold.position);
        base.TakeIngredient();
    }

    protected override void OnFound(IPestleable pestleable)
    {
        _t?.Destroy();
        base.OnFound(pestleable);
    }
}
