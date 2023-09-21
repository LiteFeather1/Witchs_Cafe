using UnityEngine;

public class Ladle : MonoBehaviour
{
    [SerializeField] private Cauldron _cauldron;

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