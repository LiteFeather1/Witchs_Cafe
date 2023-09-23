using UnityEngine;

public abstract class IngredientBehaviour : MonoBehaviour, IIngredient
{
    [SerializeField] protected IngredientSO _ingredient;

    public string Name => _ingredient.Name;

    public IngredientSO Ingredient => _ingredient;

    public abstract void AddIngredient(Coffee coffee);

    public void Destroy() => Destroy(gameObject);
}
