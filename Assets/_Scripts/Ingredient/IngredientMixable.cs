public class IngredientMixable : IngredientBehaviour, IMixable
{
    public override void AddIngredient(Coffee coffee)
    {
        coffee.AddIngredient(_ingredient);
    }
}

