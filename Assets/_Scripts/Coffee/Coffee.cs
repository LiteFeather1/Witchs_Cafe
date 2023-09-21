using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Coffee
{
    [SerializeField] private IngredientSO _coffeeBean;
    [SerializeField] private IngredientSO _milk;
    [SerializeField] private List<IngredientSO> _miscIngredients = new();

    public Action<IngredientSO> OnCoffeeBeanSet { get; set; }

    public IngredientSO CoffeeBean => _coffeeBean;
    public IngredientSO Milk => _milk;
    public List<IngredientSO> MiscIngredients => _miscIngredients;

    public void SetCoffee(IngredientSO coffeeBean)
    {
        _coffeeBean = coffeeBean;
        OnCoffeeBeanSet?.Invoke(coffeeBean);
    }

    public void SetMilk(IngredientSO milk)
    {
        _milk = milk;
    }

    public void AddIngredient(IngredientSO ingredient)
    {
        if (_coffeeBean == null)
            return;

        if (_miscIngredients.Contains(ingredient))
            return;

        _miscIngredients.Add(ingredient);
    }

    public void RemoveIngredient(IngredientSO ingredient)
    {
        if (!_miscIngredients.Contains(ingredient))
            return;

        _miscIngredients.Remove(ingredient);
    }
}
