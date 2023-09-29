public class IngredientTopping : IngredientBehaviour, ITopping
{
    public override void AddIngredient(Coffee coffee) => coffee.AddIngredient(_ingredient);
}

