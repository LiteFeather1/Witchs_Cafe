using UnityEngine;

public class Cauldron : ReceiveIngredient<IMixable>
{
    [SerializeField] private Coffee _mixingCoffee;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer[] _waterSprites;

    public Coffee MixingCoffee => _mixingCoffee;

    private void OnEnable()
    {
        SubToCoffee();
    }

    private void OnDisable()
    {
        UnSubToCoffee();
    }

    public void SubToCoffee()
    {
        _mixingCoffee.OnCoffeeBeanSet += OnCoffeeBeanSet;
    }

    public void UnSubToCoffee()
    {
        _mixingCoffee.OnCoffeeBeanSet -= OnCoffeeBeanSet;
    }

    // Also Called by a unity event
    public void TrashCoffee()
    {
        UnSubToCoffee();
        _mixingCoffee = new();
        SubToCoffee();
    }

    protected override void TakeIngredient()
    {
        _t.AddIngredient(_mixingCoffee);
        _t.Destroy();
        base.TakeIngredient();
    }

    public void OnCoffeeBeanSet(IngredientSO ingredient)
    {
        for (int i = 0; i < _waterSprites.Length; i++)
        {
            _waterSprites[i].color = ingredient.Colour;
        }
    }
}
