public class CoffeeComparisonResults
{
    public Coffee Deliver { get; private set; }
    public float Money { get; set; }
    public float Equality { get; private set; }

    public CoffeeComparisonResults(float money, float equality, Coffee deliver)
    {
        Money = money;
        Equality = equality;
        Deliver = deliver;
    }

    public CoffeeComparisonResults(Coffee order, Coffee deliver)
    {
        float money = 0;
        float equality = 0f;
        float percentPerEqual = 100f / (order.MiscIngredients.Count + 2);

        if (order.CoffeeBean == deliver.CoffeeBean)
        {
            money += order.CoffeeBean.Money;
            equality += percentPerEqual;
        }

        if (order.Milk == deliver.Milk)
        {
            if (deliver.Milk != null)
                money += order.Milk.Money;
            equality += percentPerEqual;
        }

        for (int i = 0; i < deliver.MiscIngredients.Count; i++)
        {
            var ingredient = deliver.MiscIngredients[i];
            if (!order.MiscIngredients.Contains(ingredient))
                continue;

            money += ingredient.Money;
            equality += percentPerEqual;
        }

        Money = money;
        Equality = equality;
        Deliver = deliver;
    }

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.Append(Deliver).AppendLine();
        sb.Append("Money: ").Append(Money).AppendLine();
        sb.Append("Equality: ").Append(Equality).AppendLine();
        return sb.ToString();
    }
}