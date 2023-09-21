using UnityEngine;

[CreateAssetMenu(fileName = NEW_INGREDIENT)]
public class IngredientSO : ScriptableObject
{
    public const string NEW_INGREDIENT = "New Ingredient";

    [SerializeField] private string _name = NEW_INGREDIENT;
    [SerializeField] private Color _colour = Color.white;
    [SerializeField] private int _money;

    public string Name => _name;
    public Color Colour => _colour;
    public int Money => _money;
}
