using UnityEngine;

[CreateAssetMenu(fileName = NEW_INGREDIENT)]
public class IngredientSO : ScriptableObject
{
    public const string NEW_INGREDIENT = "New Ingredient";
    [SerializeField] private TranslatedString _name;
    [SerializeField] private Color _colour = Color.white;
    [SerializeField] private float _money;

    public TranslatedString Name => _name;
    public Color Colour => _colour;
    public float Money => _money;
}
