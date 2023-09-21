using UnityEngine;

public class Client : MonoBehaviour
{
    [Header("Coffee")]
    [SerializeField] private string _name;
    [SerializeField, TextArea] private string _dialogue;
    [SerializeField] private CoffeeOrder _coffeeOrder;

    public string Name => _name;
    public string Dialogue => _dialogue;
    public CoffeeOrder CoffeOrder => _coffeeOrder;
}

