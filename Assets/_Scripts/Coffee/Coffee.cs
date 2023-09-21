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

    public CoffeeComparisonResults Compare(Coffee order, Coffee deliver)
    {
        int money = 0;
        float equality = 0f;
        float percentPerEqual = 100f/ (order._miscIngredients.Count + 2);

        if (order._coffeeBean == deliver._coffeeBean)
        {
            money += order._coffeeBean.Money;
            equality += percentPerEqual;
        }

        if (order._milk == deliver._milk)
        {
            money += order._milk.Money;
            equality += percentPerEqual;
        }

        for (int i = 0; i < deliver._miscIngredients.Count; i++)
        {
            var ingredient = deliver._miscIngredients[i];
            if (!order._miscIngredients.Contains(ingredient))
                continue;

            money += ingredient.Money;
            equality += percentPerEqual;
        }

        return new(money, equality, deliver);
    }
}
