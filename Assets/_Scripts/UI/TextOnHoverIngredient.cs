using UnityEngine;

public class TextOnHoverIngredient : TextOnHover
{
    [SerializeField] private IngredientBehaviour _ingredientBehaviour;

    protected override string Text => _ingredientBehaviour.Ingredient.Name;
}
