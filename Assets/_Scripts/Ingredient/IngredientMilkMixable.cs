public class IngredientMilkMixable : IngredientBehaviour, IMixable
{
    public override void AddIngredient(Coffee coffee)
    {
        coffee.SetMilk(_ingredient);
    }
}

