using UnityEngine;

public class TextOnHoverIngredient : TextOnHoverCollider
{
    [SerializeField] private IngredientBehaviour _ingredientBehaviour;

    protected override string Text => _ingredientBehaviour.Ingredient.Name;
}
