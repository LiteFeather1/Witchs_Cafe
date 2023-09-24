using UnityEngine;

public class Laddle : MonoBehaviour
{
    [SerializeField] private Cauldron _cauldron;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer _waterSprite;

    private void OnEnable() => _cauldron.OnCoffeChange += SetWaterColour;

    private void OnDisable() => _cauldron.OnCoffeChange -= SetWaterColour;

    private void SetWaterColour(Color colour) => _waterSprite.color = colour;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_cauldron.MixingCoffee.CoffeeBean == null)
            return;

        if (!collision.TryGetComponent(out CoffeeCup coffeeCup))
            return;

        if (coffeeCup.ReceiveCoffee(_cauldron.MixingCoffee))
            _cauldron.TrashCoffee();
    }
}