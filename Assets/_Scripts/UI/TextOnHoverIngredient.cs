using UnityEngine;

public class TextOnHoverIngredient : TextOnHoverCollider
{
    [SerializeField] private IngredientBehaviour _ingredientBehaviour;

    protected override TranslatedString Text => _ingredientBehaviour.Ingredient.Name;
}
