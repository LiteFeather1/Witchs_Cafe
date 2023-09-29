public class IngredientCoffeeMixable : IngredientBehaviour, IMixable
{
    public override void AddIngredient(Coffee coffee) => coffee.SetCoffee(_ingredient);
}

